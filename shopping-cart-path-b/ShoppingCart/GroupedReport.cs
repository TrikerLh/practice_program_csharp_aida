using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class GroupedReport
{
    private readonly ProductList _productList;

    public GroupedReport(ProductList productList)
    {
        _productList = productList;
    }

    public IEnumerable<ReportLine> GetItems()
    {
        return _productList
            .CreateItems()
            .GroupBy(p => p.Name)
            .Select(CreateItem);
    }

    private ReportLine CreateItem(IGrouping<string, ReportLine> productGrouping)
    {
        var name = productGrouping.Key;
        var quantity = productGrouping.Count();
        var totalCost = productGrouping.First().TotalCost * quantity;
        return new ReportLine(name, quantity, totalCost);
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

    public bool HasDiscount()
    {
        return _productList.HasDiscount();
    }
}