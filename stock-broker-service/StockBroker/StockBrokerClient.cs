using System;
using System.Collections.Generic;
using System.Globalization;

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
        var orders = GetOrder(orderSequence);
        var orderFail = "";
        foreach (var order in orders)
        {
            var exitPlace = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
            if (!exitPlace) {
                orderFail += order.GetSymbol();
            }
        }
        var summary = _summaryFormatter.GetFormatSummary(time, orders, orderFail);
        _notifier.Notify(summary);
    }
    private List<Order> GetOrder(string orderSequence) {
        var orders = new List<Order>();
        if (string.IsNullOrEmpty(orderSequence)) {
            orders.Add(new Order("", 0, 0.0, ""));
            return orders;
        }
        var ordersSplit = orderSequence.Split(',');
        foreach (var order in ordersSplit)
        {
            var symbol = order.Split(" ")[0];
            var quantity = int.Parse(order.Split(" ")[1]);
            var price = double.Parse(order.Split(" ")[2], CultureInfo.InvariantCulture);
            var type = order.Split(" ")[3];
            orders.Add(new Order(symbol, quantity, price, type));
        }
        return orders;
    }
}

internal class SummaryFormatter
{
    private readonly CultureInfo _cultureInfo;

    public SummaryFormatter(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    internal string GetFormatSummary(DateTime time, IList<Order> orders, string orderFail) {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (orders.Count == 0) {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        if (string.IsNullOrEmpty(orderFail)) {
            var buyAmount = 0.0;
            var sellAmount = 0.0;
            foreach (var order in orders) {
                buyAmount += order.GetBuyAmount();
                sellAmount += order.GetSellAmount();
            }
            return timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
        }
        return timeFormated + " Buy: € 0.00, Sell: € 0.00, Failed: " + orderFail;
    }

    private string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}