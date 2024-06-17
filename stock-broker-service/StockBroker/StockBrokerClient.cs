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
        var orders = Order.GetOrders(orderSequence);
        foreach (var order in orders) {
            var success = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
            if (!success) {
                order.ToFail();
            }
        }

        var summary = _summaOrderFormatter.GetFormatSummary(orders);
        _notifier.Notify(summary);
    }
}