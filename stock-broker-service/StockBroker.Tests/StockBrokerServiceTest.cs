using System;
using System.Globalization;
using System.Linq.Expressions;
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

            var summary = "6/11/2024 12:20 PM Buy:  0.00, Sell:  0.00";
            SpyMessage(summary);
        }

        [TestCase(10.00)]
        [TestCase(58.00)]
        public void Place_a_Buy_order_for_one_stock(decimal price)
        {
            GetDateTimeForOrder(2022, 05, 14, 13, 54);

            PlaceOrdersSequence($"GOOG 1 {price} B");

            var summary = $"5/14/2022 1:54 PM Buy:  {FormatDecimal(price)}, Sell:  0.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_a_Buy_order_for_two_stock() {
            GetDateTimeForOrder(2015, 5, 3, 5, 07);

            PlaceOrdersSequence("KO 2 10.00 B");

            var summary = "5/3/2015 5:07 AM Buy:  20.00, Sell:  0.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_a_Sell_order_for_four_stock() {
            GetDateTimeForOrder(2023, 12, 31, 23, 54);

            PlaceOrdersSequence("AAPL 4 10.00 S");

            var summary = "12/31/2023 11:54 PM Buy:  0.00, Sell:  40.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_a_Buy_order_with_fail()
        {
            GetDateTimeForOrder(2020, 04,06,17,24);
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(false);

            PlaceOrdersSequence("IBE 4 58.63 B");

            var summary = "4/6/2020 5:24 PM Buy:  0.00, Sell:  0.00, Failed: IBE";
            SpyMessage(summary);
        }

        [Test]
        public void Place_two_Buy_orders() {
            GetDateTimeForOrder(2024, 5, 11, 17, 35);

            PlaceOrdersSequence("BROP 2 15.00 B,BULL 5 50.00 B");

            var summary = "5/11/2024 5:35 PM Buy:  280.00, Sell:  0.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_two_Sell_orders() {
            GetDateTimeForOrder(2024, 5, 11, 17, 35);

            PlaceOrdersSequence("POPP 3 5.00 S,DAI 10 23.00 S");

            var summary = "5/11/2024 5:35 PM Buy:  0.00, Sell:  245.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_one_Buy_order_and_one_Sell_order() {
            GetDateTimeForOrder(2024, 5, 11, 17, 35);

            PlaceOrdersSequence("POPP 3 5.00 B,DAI 10 23.00 S");

            var summary = "5/11/2024 5:35 PM Buy:  15.00, Sell:  230.00";
            SpyMessage(summary);
        }

        [Test]
        public void Place_two_Buy_orders_with_two_errors() {
            GetDateTimeForOrder(2024, 5, 11, 17, 35);
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(false);

            PlaceOrdersSequence("POPP 3 5.00 B,DAI 10 23.00 B");

            var summary = "5/11/2024 5:35 PM Buy:  0.00, Sell:  0.00, Failed: POPP, DAI";
            SpyMessage(summary);
        }

        [Ignore("refactor")]
        [Test]
        public void Place_two_Buy_orders_with_one_error() {
            GetDateTimeForOrder(2024, 5, 11, 17, 35);
            _stockBrokerService.Place(Arg.Any<OrderDTO>()).Returns(false, true);

            PlaceOrdersSequence("POPP 3 5.00 B,DAI 10 23.00 B");

            var summary = "5/11/2024 5:35 PM Buy:  0.00, Sell:  230.00, Failed: POPP";
            SpyMessage(summary);
        }

        private void SpyMessage(string summary)
        {
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
 * Ejemplo 5: input: "" output: 8/15/2019 2:45 PM Buy:  0.00, Sell:  0.00 OK
 * Ejemplo 1: input: "GOOG 1 10.00 B" output: 7/25/2008 3:45 PM Buy:  10.00, Sell:  0.00
 *            input: "GOOG 1 10.00 S" output: 7/25/2008 3:45 PM Buy:  0.00, Sell:  10.00
 *            input: "GOOG 2 10.00 B" output: 7/25/2008 3:45 PM Buy:  20.00, Sell:  0.00
 *            input: "GOOG 2 10.00 S" output: 7/25/2008 3:45 PM Buy:  0.00, Sell:  20.00
 * Ejemplo 2: input: "ZNGA 1 10.00 B,AAPL 1 10.00 S" output: 6/15/2009 1:45 PM Buy:  10.00, Sell:  10.00
 *            input: "ZNGA 1 10.00 B,AAPL 1 10.00 B" output: 6/15/2009 1:45 PM Buy:  20.00, Sell:  0.00
 *            input: "ZNGA 1 10.00 S,AAPL 1 10.00 S" output: 6/15/2009 1:45 PM Buy:  00.00, Sell:  20.00
 * Si acabo sigo con mαs, tenemos que meter tambiιn el ooops con 1 y con many.
 * Los cσdigos y las fechas se pueden ir cambiando.
 * ΑNIMO!
 */