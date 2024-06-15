using System;
using System.Collections.Generic;
using System.Globalization;

namespace StockBroker;

internal class SummaryFormatter
{
    private readonly CultureInfo _cultureInfo;

    public SummaryFormatter(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    internal string GetFormatSummary(DateTime time, IList<Order> orders, List<string> orderFails) {
        var timeFormated = time.ToString("g", _cultureInfo);
        if (orders.Count == 0) {
            return timeFormated + " Buy: € 0.00, Sell: € 0.00";
        }

        if (orderFails.Count == 0) {
            var buyAmount = 0.0;
            var sellAmount = 0.0;
            foreach (var order in orders) {
                buyAmount += order.GetBuyAmount();
                sellAmount += order.GetSellAmount();
            }
            return timeFormated + $" Buy: {FormatAmount(buyAmount)}, Sell: {FormatAmount(sellAmount)}";
        }
        return timeFormated + " Buy: € 0.00, Sell: € 0.00, Failed: " + GetFailsFormatter(orderFails);
    }

    private string GetFailsFormatter(List<string> orderFails)
    {
        var fails = "";
        for (var i = 0; i < orderFails.Count; i++)
        {
            var orderFail = orderFails[i];
            if (i == 0)
            {
                fails += orderFail;
            }
            else {
                fails += ", " + orderFail;
            }
        }
        return fails;
    }

    private string FormatAmount(double amount) {
        return "€ " + amount.ToString("F2", _cultureInfo);
    }
}