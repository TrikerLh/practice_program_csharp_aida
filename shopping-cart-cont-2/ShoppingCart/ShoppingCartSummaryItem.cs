namespace ShoppingCart;

public class ShoppingCartSummaryItem {
    private readonly Product _product;
    public ShoppingCartSummaryItem(Product _product) {
        this._product = _product;
    }

    public decimal TotalCost() {
        return _product.ComputeCost() ;
    }

    public string Name() {
        return _product.ProductName;
    }
}
