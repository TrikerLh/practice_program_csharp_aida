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
            _stockBrokerClient = new StockBrokerClient(_dateTimeProvider, _notifier, _stockBrokerService);
        }
        [Test]
        public void With_a_empty_order()
        {
            _dateTimeProvider.GetDateTime().Returns(new DateTime(2024, 06, 11, 12, 20, 00));
            var orderSequence = "";

            _stockBrokerClient.PlaceOrder(orderSequence);

            var time = new DateTime(2024, 06, 11, 12, 20, 00);
            var summary = time.ToString("g", new CultureInfo("en-US")) + " Buy: \u20ac 0.00, Sell: \u20ac 0.00";
            _notifier.Received(1).Notify(summary);
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