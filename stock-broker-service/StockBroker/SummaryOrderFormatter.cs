using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

internal class SummaryOrderFormatter
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly CultureInfo _cultureInfo;

    public SummaryOrderFormatter(DateTimeProvider dateTimeProvider, CultureInfo cultureInfo)
    {
        _dateTimeProvider = dateTimeProvider;
        _cultureInfo = cultureInfo;
    }

    internal string GetFormatSummary(List<Order> orders)
    {
        var time = _dateTimeProvider.GetDateTime();
        var timeFormated = time.ToString("g", _cultureInfo);
        if (orders.Count == 0)
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        var buyAmount = 0.0;
        var sellAmount = 0.0;
        foreach (var order in orders)
        {
            if (order.IsSuccess())
            {
                buyAmount += order.GetBuyAmount();
                sellAmount += order.GetSellAmount();
            }
            else
            {
                return timeFormated + " Buy: € 0.00, Sell: € 0.00, Failed: " + order.GetSymbol();
            }
        }

        return timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
    }

    private string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}