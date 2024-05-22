using System.Collections.Generic;

namespace ShoppingCart;

internal class ShoppingCartSummary {
    private readonly List<Product> _products;
    private readonly decimal _totalPrice;

    public ShoppingCartSummary(List<Product> products, decimal totalPrice) {
        _products = products;
        _totalPrice = totalPrice;
    }

    public int TotalProducts() {
        return _products.Count;
    }

    public decimal TotalPrice() {
        return _totalPrice;
    }

    public decimal ProductsTotalCost() {
        return _products[0].ComputeCost() * TotalProducts();
    }

    public string ProductName() {
        return _products[0].ProductName;
    }
}