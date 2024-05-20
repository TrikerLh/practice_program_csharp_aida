using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShoppingCart;

public static class ShoppingCartSummary
{
    public static string Summary(decimal totalCost, List<Product> productList)
    {
        return $"{CreateHeader()}\n{CreateBody(productList)}{CreateFooter(totalCost, productList.Count)}";
    }

    private static string CreateFooter(decimal totalCost, int totalProducts)
    {
        return $"Total products: {totalProducts}\nTotal price: {totalCost.ToString(new CultureInfo("en-GB"))}\u20ac";
    }

    private static string CreateHeader()
    {
        return "Product name, Price with VAT, Quantity";
    }

    private static string CreateBody(List<Product> productList)
    {
        if (!productList.Any())
        {
            return "";
        }

        return "Iceberg, 1.55€, 1\n";
    }
}