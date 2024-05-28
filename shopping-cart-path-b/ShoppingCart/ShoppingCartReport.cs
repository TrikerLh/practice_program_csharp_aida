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

    public IEnumerable<ShoppingCartSummaryItem> GetItems()
    {
        return _productList
            .CreateItems()
            .GroupBy(p => p.Name)
            .Select(CreateItem);
    }

    private ShoppingCartSummaryItem CreateItem(IGrouping<string, ShoppingCartSummaryItem> productGrouping)
    {
        var name = productGrouping.Key;
        var quantity = productGrouping.Count();
        var totalCost = productGrouping.First().TotalCost * quantity;
        return new ShoppingCartSummaryItem(name, quantity, totalCost);
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