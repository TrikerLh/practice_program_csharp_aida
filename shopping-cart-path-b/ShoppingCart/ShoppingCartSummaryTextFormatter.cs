using System.Linq;

namespace ShoppingCart;

public class ShoppingCartSummaryTextFormatter
{
    private const string Header = "Product name, Price with VAT, Quantity\n";
    private readonly ShoppingCartReport _report;

    public ShoppingCartSummaryTextFormatter(ProductList productList)
    {
        _report = new ShoppingCartReport(productList);
    }

    public string Format()
    {
        return $"{Header}" +
               $"{CreateBody(_report)}" +
               $"{CreateFooter(_report)}";
    }

    private string CreateFooter(ShoppingCartReport report)
    {
        return $"Total products: {report.TotalProducts()}\nTotal price: {report.GetTotalPrice()}\u20ac";
    }

    private string CreateBody(ShoppingCartReport report)
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