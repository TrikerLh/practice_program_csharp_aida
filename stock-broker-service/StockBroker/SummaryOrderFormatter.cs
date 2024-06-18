using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

internal class SummaryOrderFormatter
{
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly CultureInfo _cultureInfo;
    private readonly List<Order> _orders;

    public SummaryOrderFormatter(DateTimeProvider dateTimeProvider, CultureInfo cultureInfo, List<Order> orders)
    {
        _dateTimeProvider = dateTimeProvider;
        _cultureInfo = cultureInfo;
        _orders = orders;
    }

    internal string GetFormatSummary()
    {
        var time = _dateTimeProvider.GetDateTime();
        var timeFormated = time.ToString("g", _cultureInfo);

        var buyAmount = 0.0;
        var sellAmount = 0.0;
        var summaryFail = new List<string>();
        foreach (var order in _orders)
        {
            if (order.IsSuccess())
            {
                buyAmount += order.GetBuyAmount();
                sellAmount += order.GetSellAmount();
            }
            else
            {
                summaryFail.Add(order.GetSymbol());
            }
        }
        var summary = CreateSummaryMessage(timeFormated, buyAmount, sellAmount, CreateSummaryFail(summaryFail));
        return summary;
    }

    private string CreateSummaryFail(List<string> summaryFail)
    {
        if (summaryFail.Count == 0)
        {
            return "";
        }

        return ", Failed: " + string.Join(", ", summaryFail);
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