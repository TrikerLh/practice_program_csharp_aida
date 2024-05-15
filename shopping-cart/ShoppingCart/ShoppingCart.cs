namespace ShoppingCart;

public class ShoppingCart
{
    private readonly ProductRepository _productRepository;
    private readonly DiscountRepository _discountRepository;
    private readonly CheckoutService _checkoutService;
    private readonly Display _display;

    public ShoppingCart(ProductRepository productRepository, DiscountRepository discountRepository, CheckoutService checkoutService, Display display)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _checkoutService = checkoutService;
        _display = display;
    }

    public void AddItem(string productName)
    {
        _display.Show("Producto no disponible.");
    }
}