using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ShoppingCartSummary
{
    private readonly ProductList _productList;
    public decimal TotalPrice { get; }
    public IEnumerable<ShoppingCartSummaryItem> Items { get; }

    public ShoppingCartSummary(ProductList productList)
    {
        _productList = productList;
        TotalPrice = productList.ComputeTotalCost();
        Items = _productList.CreateItems();
    }
    
    public int TotalProducts() {
        return _productList.TotalQuantity();
    }
}