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

        var buyAmount = 0.0;
        var sellAmount = 0.0;
        var summaryFail = "";
        foreach (var order in orders)
        {
            if (order.IsSuccess())
            {
                buyAmount += order.GetBuyAmount();
                sellAmount += order.GetSellAmount();
            }
            else
            {
                summaryFail = CreateSummaryFail(summaryFail, order);
            }
        }
        var summary = CreateSummaryMessage(timeFormated, buyAmount, sellAmount, summaryFail);
        return summary;
    }

    private string CreateSummaryFail(string summaryFail, Order order)
    {
        if (string.IsNullOrEmpty(summaryFail))
        {
            summaryFail += ", Failed: " + order.GetSymbol();
        }
        else
        {
            summaryFail += ", " + order.GetSymbol();
        }

        return summaryFail;
    }

    private string CreateSummaryMessage(string timeFormated, double buyAmount, double sellAmount, string summaryFail)
    {
        var summary = timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
        if (!string.IsNullOrEmpty(summaryFail))
        {
            summary += summaryFail;
        }

        return summary;
    }

    private string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}