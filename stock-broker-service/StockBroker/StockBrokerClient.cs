using System;
using System.Globalization;
using Microsoft.VisualBasic;

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
        var exitPlace = _stockBrokerService.Place(new OrderDTO(order.GetSymbol(), order.GetQuantity()));
        var orderFail = "";
        if (!exitPlace)
        {
            orderFail = order.GetSymbol();
        }
        var summary = GetFormatSummary(time, order, orderFail);
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

    private static string GetFormatSummary(DateTime time, Order order, string orderFail)
    {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (order.IsEmptyOrder())
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        if (string.IsNullOrEmpty(orderFail))
        {
            return timeFormated + $" Buy: {FormatAmount(order.GetBuyAmount())}, Sell: {FormatAmount(order.GetSellAmount())}";
        }
        return timeFormated + " Buy: € 0.00, Sell: € 0.00, Failed: " + orderFail;
    }

    private static string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}

public record OrderDTO(string symbol, int quantity) {
}