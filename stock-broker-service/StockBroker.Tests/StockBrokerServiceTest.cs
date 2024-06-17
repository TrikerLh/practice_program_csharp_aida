using System.Globalization;
using NSubstitute;
using NUnit.Framework;


namespace StockBroker.Tests
{
    public class StockBrokerServiceTest
    {
        private DateTimeProvider _dateTimeProvider;
        private Notifier _notifier;
        private StockBrokerService _stockBrokerService;
        private StockBrokerClient _stockBrokerClient;

        [SetUp]
        public void Setup()
        {
            _dateTimeProvider = Substitute.For<DateTimeProvider>();
            _notifier = Substitute.For<Notifier>();
            _stockBrokerService = Substitute.For<StockBrokerService>();
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(true);
            _stockBrokerClient = new StockBrokerClient(_dateTimeProvider, _notifier, _stockBrokerService);
        }
        [Test]
        public void Place_an_empty_order()
        {
            GetDateTimeForOrder(2024, 06, 11, 12, 20);

            PlaceOrdersSequence("");

            var summary = "6/11/2024 12:20 PM Buy: € 0.00, Sell: € 0.00";
            _notifier.Received(1).Notify(summary);
        }

        [TestCase(10.00)]
        [TestCase(58.00)]
        public void Place_a_Buy_order_for_one_stock(decimal price)
        {
            GetDateTimeForOrder(2022, 05, 14, 13, 54);

            PlaceOrdersSequence($"GOOG 1 {price} B");

            var summary = $"5/14/2022 1:54 PM Buy: € {FormatDecimal(price)}, Sell: € 0.00";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_a_Buy_order_for_two_stock() {
            GetDateTimeForOrder(2015, 5, 3, 5, 07);

            PlaceOrdersSequence("KO 2 10.00 B");

            var summary = "5/3/2015 5:07 AM Buy: € 20.00, Sell: € 0.00";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_a_Sell_order_for_four_stock() {
            GetDateTimeForOrder(2023, 12, 31, 23, 54);

            PlaceOrdersSequence("AAPL 4 10.00 S");

            var summary = "12/31/2023 11:54 PM Buy: € 0.00, Sell: € 40.00";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_a_Buy_order_with_error()
        {
            GetDateTimeForOrder(2000, 01, 01, 00, 01);
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(false);

            PlaceOrdersSequence("DBZ 15 28.25 B");

            var summary = "1/1/2000 12:01 AM Buy: € 0.00, Sell: € 0.00, Failed: DBZ";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_two_Buy_orders()
        {
            GetDateTimeForOrder(2021, 01, 23, 23, 30);

            PlaceOrdersSequence("NTC 15 10.00 B,OTM 5 20.00 B");

            var summary = "1/23/2021 11:30 PM Buy: € 250.00, Sell: € 0.00";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_one_buy_order_and_one_sell_order() {
            GetDateTimeForOrder(2023, 11, 2, 21, 45);

            PlaceOrdersSequence("LTC 100 5.00 B,RCC 25 25.00 S");

            var summary = "11/2/2023 9:45 PM Buy: € 500.00, Sell: € 625.00";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_one_buy_order_and_one_sell_order_with_one_fail() {
            GetDateTimeForOrder(2024, 06, 17, 22, 00);
            _stockBrokerService.Place(new OrderDTO("AIDA", 852)).Returns(false);

            PlaceOrdersSequence("AIDA 852 54.25 B,DAG 250 30.00 S");

            var summary = "6/17/2024 10:00 PM Buy: € 0.00, Sell: € 7500.00, Failed: AIDA";
            _notifier.Received(1).Notify(summary);
        }

        [Test]
        public void Place_one_buy_order_and_one_sell_order_with_two_fails() {
            GetDateTimeForOrder(2024, 06, 17, 22, 00);
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(false);

            PlaceOrdersSequence("TYUR 852 54.25 B,PDER 250 30.00 S");

            var summary = "6/17/2024 10:00 PM Buy: € 0.00, Sell: € 0.00, Failed: TYUR, PDER";
            _notifier.Received(1).Notify(summary);
        }

        private void GetDateTimeForOrder(int year, int month, int day, int hour, int minute)
        {
            _dateTimeProvider.GetDateTime().Returns(new DateTime(year, month, day, hour, minute, 00));
        }

        private void PlaceOrdersSequence(string ordersSequence)
        {
            _stockBrokerClient.PlaceOrder(ordersSequence);
        }

        private static string FormatDecimal(decimal price)
        {
            return price.ToString("F2", new CultureInfo("en-US"));
        }
    }
}