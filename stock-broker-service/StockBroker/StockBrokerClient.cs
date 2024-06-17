using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private readonly SummaryOrderFormatter _summaOrderFormatter;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService)
    {
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
        _summaOrderFormatter = new SummaryOrderFormatter(dateTimeProvider, new CultureInfo("en-US"));
    }

    public void PlaceOrder(string orderSequence)
    {
        var order = GetOrder(orderSequence);
        var summary = _summaOrderFormatter.GetFormatSummary(order);
        _notifier.Notify(summary);
    }
    private Order GetOrder(string orderSequence) {
        if (string.IsNullOrEmpty(orderSequence)) {
            return new Order("", 0, 0.0, "");
        }
        var symbol = orderSequence.Split(" ")[0];
        var quantity = int.Parse(orderSequence.Split(" ")[1]);
        var price = double.Parse(orderSequence.Split(" ")[2], CultureInfo.InvariantCulture);
        var type = orderSequence.Split(" ")[3];
        var order = new Order(symbol, quantity, price, type);
        return order;
    }
}