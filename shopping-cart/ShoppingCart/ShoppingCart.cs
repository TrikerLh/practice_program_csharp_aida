using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ShoppingCart
{
    private readonly ProductRepository _productRepository;
    private readonly DiscountRepository _discountRepository;
    private readonly CheckoutService _checkoutService;
    private readonly Display _display;
    private List<Product> products;

    public ShoppingCart(ProductRepository productRepository, DiscountRepository discountRepository, CheckoutService checkoutService, Display display)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _checkoutService = checkoutService;
        _display = display;
        products = new List<Product>();
    }

    public void AddItem(string productName)
    {
        var product = _productRepository.Get(productName);  

        if (product == null)
        {
            _display.Show("Producto no disponible.");
        }

        products.Add(product);
    }

    public void Checkout()
    {
        var lines = products.Select(p => new LineDto(p.Name, p.Cost, 1)).ToList();
        _checkoutService.Checkout(new ShoppingCartDto(lines, 0, lines.Sum(p => p.Cost)));
    }
}