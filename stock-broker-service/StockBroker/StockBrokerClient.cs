using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private readonly SummaryFormatter _summaryFormatter;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService)
    {
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
        _summaryFormatter = new SummaryFormatter(new CultureInfo("en-US"));
    }

    public void PlaceOrder(string orderSequence)
    {
        var time = _dateTimeProvider.GetDateTime();
        var orders = GetOrders(orderSequence);
        var orderFails = new List<string>();
        foreach (var order in orders)
        {
            var exitPlace = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
            if (!exitPlace) {
                orderFails.Add(order.GetSymbol());
            }
        }
        var summary = _summaryFormatter.GetFormatSummary(time, orders, orderFails);
        _notifier.Notify(summary);
    }
    private List<Order> GetOrders(string orderSequence) {
        var orders = new List<Order>();
        if (string.IsNullOrEmpty(orderSequence)) {
            orders.Add(new Order("", 0, 0.0, ""));
            return orders;
        }
        var ordersSplit = orderSequence.Split(',');
        orders.AddRange(
            ordersSplit.Select(
                order => new Order(
                    order.Split(" ")[0], 
                    int.Parse(order.Split(" ")[1]), 
                    double.Parse(order.Split(" ")[2], CultureInfo.InvariantCulture), 
                    order.Split(" ")[3]
                    )
                )
            );
        return orders;
    }
}