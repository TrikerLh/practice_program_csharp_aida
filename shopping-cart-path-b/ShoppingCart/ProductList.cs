using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ProductList
{
    private readonly List<Product> _productList;
    private Discount _discount;

    public ProductList()
    {
        _productList = new List<Product>();
        _discount = new Discount(DiscountCode.None, 0);
    }

    public bool ThereAreNoProducts()
    {
        return !_productList.Any();
    }

    public decimal ComputeAllProductsCost()
    {
        return _productList.Sum(p => p.ComputeCost());
    }

    public void AddProduct(Product product)
    {
        _productList.Add(product);
    }

    public decimal ComputeTotalCost()
    {
        var totalCost = this.ComputeAllProductsCost();
        return _discount.Apply(totalCost);
    }

    public void AddDiscount(Discount discount)
    {
        _discount = discount;
    }

    public int TotalQuantity()
    {
        return _productList.Count;
    }

    private ReportLine CreateItem(Product product)
    {
        return new ReportLine(product.ProductName, 1, product.ComputeCost());
    }

    public IEnumerable<ReportLine> CreateItems()
    {
        return _productList
            .Select(CreateItem);
    }

    public bool HasDiscount()
    {
        return _discount.HasDiscount();
    }
}