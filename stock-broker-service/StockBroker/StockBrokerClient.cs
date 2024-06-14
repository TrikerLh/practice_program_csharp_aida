using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;
    private static CultureInfo _cultureInfo;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService)
    {
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
        _cultureInfo = new CultureInfo("en-US");
    }

    public void PlaceOrder(string orderSequence)
    {
        var time = _dateTimeProvider.GetDateTime();
        var order = GetOrder(orderSequence);
        var summary = GetFormatSummary(time, order);
        _notifier.Notify(summary);
    }
    private Order GetOrder(string orderSequence) {
        if (string.IsNullOrEmpty(orderSequence)) {
            return new Order("", 0, 0.0, "");
        }
        var symbol = orderSequence.Split(" ")[0];
        var quantity = int.Parse(orderSequence.Split(" ")[1]);
        var price = double.Parse(orderSequence.Split(" ")[2], _cultureInfo);
        var type = orderSequence.Split(" ")[3];
        var order = new Order(symbol, quantity, price, type);
        return order;
    }

    //TODO: es static, podría ir en una clase formater?

    private static string GetFormatSummary(DateTime time, Order order)
    {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (order.IsEmptyOrder())
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }
        return timeFormated + $" Buy: {FormatAmount(order.GetBuyAmount())}, Sell: {FormatAmount(order.GetSellAmount())}";
    }

    private static string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}