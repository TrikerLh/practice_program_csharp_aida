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

        var quantity = int.Parse(orderSequence.Split(" ")[1]);
        var price = double.Parse(orderSequence.Split(" ")[2], _cultureInfo);
        var type = orderSequence.Split(" ")[3];
        var buyAmount = 0.0;
        var sellAmount = 0.0;
        if (type == "B")
        {
            buyAmount = price * quantity;
        }
        else
        {
            sellAmount = price * quantity;
        }
        return timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
    }

    private static string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}