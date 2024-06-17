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

    internal string GetFormatSummary(Order order)
    {
        var time = _dateTimeProvider.GetDateTime();
        var timeFormated = time.ToString("g", _cultureInfo);
        if (order.IsEmptyOrder())
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        if (!order.IsSuccess())
        {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00, Failed: " + order.GetSymbol();
        }
        return timeFormated + $" Buy: {FormatAmount(order.GetBuyAmount())}, Sell: {FormatAmount(order.GetSellAmount())}";
    }

    private string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}