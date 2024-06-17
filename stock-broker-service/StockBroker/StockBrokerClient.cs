using System;
using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient {
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private readonly SummaryOrderFormatter _summaOrderFormatter;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService) {
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
        _summaOrderFormatter = new SummaryOrderFormatter(dateTimeProvider, new CultureInfo("en-US"));
    }

    public void PlaceOrder(string orderSequence) {
        var orders = GetOrder(orderSequence);
        foreach (var order in orders) {
            var success = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
            if (!success) {
                order.ToFail();
            }
        }

        var summary = _summaOrderFormatter.GetFormatSummary(orders);
        _notifier.Notify(summary);
    }
    private List<Order> GetOrder(string orderSequence) {
        var orders = new List<Order>();
        if (string.IsNullOrEmpty(orderSequence)) {
            orders.Add(new Order("", 0, 0.0, ""));
            return orders;
        }
        var ordersSequence = orderSequence.Split(',');
        foreach (var order in ordersSequence) {
            var symbol = order.Split(" ")[0];
            var quantity = int.Parse(order.Split(" ")[1]);
            var price = double.Parse(order.Split(" ")[2], CultureInfo.InvariantCulture);
            var type = order.Split(" ")[3];
            orders.Add(new Order(symbol, quantity, price, type));
        }

        return orders;
    }
}