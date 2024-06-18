using System;
using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient {
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private SummaryOrderFormatter _summaOrderFormatter;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService) {
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
    }

    public void PlaceOrder(string orderSequence) {
        var orders = Order.GetOrders(orderSequence);
        foreach (var order in orders) {
            var success = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
            if (!success) {
                order.ToFail();
            }
        }
        _summaOrderFormatter = new SummaryOrderFormatter(_dateTimeProvider, new CultureInfo("en-US"), orders);
        var summary = _summaOrderFormatter.GetFormatSummary();
        _notifier.Notify(summary);
    }
}