using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly Notifier _notifier;
    private readonly StockBrokerService _stockBrokerService;

    public StockBrokerClient(DateTimeProvider dateTimeProvider, Notifier notifier, StockBrokerService stockBrokerService)
    {
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
        _stockBrokerService = stockBrokerService;
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
        var timeFormated = time.ToString("g", new CultureInfo("en-US"));
        if (string.IsNullOrEmpty(orderSequence))
        {
            return timeFormated + " Buy: \u20ac 0.00, Sell: \u20ac 0.00";
        }
        return timeFormated + " Buy: \u20ac 10.00, Sell: \u20ac 0.00";
    }
}