using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ShoppingCartReport
{
    private readonly ProductList _productList;

    public ShoppingCartReport(ProductList productList)
    {
        _productList = productList;
    }

    public IEnumerable<ShoppingCartReportItem> GetItems()
    {
        return _productList
            .CreateItems()
            .GroupBy(p => p.Name)
            .Select(CreateItem);
    }

    private ShoppingCartReportItem CreateItem(IGrouping<string, ShoppingCartReportItem> productGrouping)
    {
        var name = productGrouping.Key;
        var quantity = productGrouping.Count();
        var totalCost = productGrouping.First().TotalCost * quantity;
        return new ShoppingCartReportItem(name, quantity, totalCost);
    }

    public decimal GetTotalPrice()
    {
        return _productList.ComputeTotalCost();
    }

    public int TotalProducts()
    {
        return _productList.TotalQuantity();
    }

    public bool ThereAreNoProducts()
    {
        return _productList.ThereAreNoProducts();
    }
}