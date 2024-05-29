using System.Linq;

namespace ShoppingCart;

public class TextReportFormatter
{
    private const string Header = "Product name, Price with VAT, Quantity\n";
    private readonly GroupedReport _report;

    public TextReportFormatter(ProductList productList)
    {
        _report = new GroupedReport(productList);
    }

    public string Format()
    {
        return $"{Header}" +
               $"{CreateBody(_report)}" +
               $"{CreateFooter(_report)}";
    }

    private string CreateFooter(GroupedReport report)
    {
        return $"Total products: {report.TotalProducts()}\nTotal price: {report.GetTotalPrice()}\u20ac";
    }

    private string CreateBody(GroupedReport report)
    {
        if (_report.ThereAreNoProducts())
        {
            return "";
        }

        return report.GetItems()
            .Select(item => $"{item.Name}, {item.TotalCost}€, {item.Quantity}\n")
            .Aggregate("", (current, line) => current + line);
    }
}