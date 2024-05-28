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

    public int TotalQuantity() {
        return _productList.Count;
    }

    private ShoppingCartSummaryItem CreateItem(IGrouping<string, Product> productGrouping)
    {
        var name = productGrouping.Key;
        var quantity = productGrouping.Count();
        var totalCost = productGrouping.First().ComputeCost() * quantity;
        return new ShoppingCartSummaryItem(name, quantity, totalCost);
    }

    public IEnumerable<ShoppingCartSummaryItem> CreateItems()
    {
        return _productList
            .GroupBy(p => p.ProductName)
            .Select(CreateItem);
    }
}