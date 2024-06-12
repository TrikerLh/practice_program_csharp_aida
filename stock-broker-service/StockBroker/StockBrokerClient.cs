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
        var summary = GetFormatSummary(time, orderSequence);
        _notifier.Notify(summary);
    }

    //TODO: es static, podría ir en una clase formater?
    private static string GetFormatSummary(DateTime time, string orderSequence)
    {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (string.IsNullOrEmpty(orderSequence))
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        var price = decimal.Parse(orderSequence.Split(" ")[2], _cultureInfo);
        var quantity = int.Parse(orderSequence.Split(" ")[1]);
        return timeFormated + $" Buy: {FormatAmount(price, quantity)}, Sell: € 0.00";
    }

    private static string FormatAmount(decimal price, int quantity) {
        var amount = price * quantity;
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}