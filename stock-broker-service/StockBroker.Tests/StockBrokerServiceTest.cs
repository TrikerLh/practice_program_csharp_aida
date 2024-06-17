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


/* Lista de ejemplos ordenados (0 - one - many - ooops)
 * Ejemplo 5: input: "" output: 8/15/2019 2:45 PM Buy: € 0.00, Sell: € 0.00 OK
 * Ejemplo 1: input: "GOOG 1 10.00 B" output: 7/25/2008 3:45 PM Buy: € 10.00, Sell: € 0.00
 *            input: "GOOG 1 10.00 S" output: 7/25/2008 3:45 PM Buy: € 0.00, Sell: € 10.00
 *            input: "GOOG 2 10.00 B" output: 7/25/2008 3:45 PM Buy: € 20.00, Sell: € 0.00
 *            input: "GOOG 2 10.00 S" output: 7/25/2008 3:45 PM Buy: € 0.00, Sell: € 20.00
 * Ejemplo 2: input: "ZNGA 1 10.00 B,AAPL 1 10.00 S" output: 6/15/2009 1:45 PM Buy: € 10.00, Sell: € 10.00
 *            input: "ZNGA 1 10.00 B,AAPL 1 10.00 B" output: 6/15/2009 1:45 PM Buy: € 20.00, Sell: € 0.00
 *            input: "ZNGA 1 10.00 S,AAPL 1 10.00 S" output: 6/15/2009 1:45 PM Buy: € 00.00, Sell: € 20.00
 * Si acabo sigo con más, tenemos que meter también el ooops con 1 y con many.
 * Los códigos y las fechas se pueden ir cambiando.
 * ÁNIMO!
 */