using System.Globalization;
using System.Linq;

namespace ShoppingCart;

public class TextReportFormatter : ReportFormatter {
    private readonly CultureInfo _cultureInfo;
    private const string Header = "Product name, Price with VAT, Quantity\n";

    public TextReportFormatter(CultureInfo cultureInfo) {
        _cultureInfo = cultureInfo;
    }

    public string Format(Report report)
    {
        return $"{Header}" +
               $"{CreateBody(report)}" +
               $"{CreatePromotion(report)}" +
               $"{CreateFooter(report)}";
    }

    private string CreatePromotion(Report report)
    {
        if (report.HasDiscount())
        {
            return $"Promotion: {report.GetDiscount()}% off with code {report.GetDiscountCode()}\n";
        }

        return "";
    }

    private string CreateFooter(Report report)
    {
        return $"Total products: {report.TotalProducts()}\nTotal price: {report.GetTotalPrice().ToString(_cultureInfo)}\u20ac";
    }

    private string CreateBody(Report report)
    {
        if (report.ThereAreNoProducts())
        {
            return "";
        }

        return report.GetItems()
            .Select(item => $"{item.Name}, {item.TotalCost.ToString(_cultureInfo)}€, {item.Quantity}\n")
            .Aggregate("", (current, line) => current + line);
    }
}