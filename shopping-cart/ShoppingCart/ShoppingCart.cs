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
        var shoppingCartDto = CreateShoppingCartDto(out var lines);
        _checkoutService.Checkout(shoppingCartDto);
    }

    private ShoppingCartDto CreateShoppingCartDto(out List<LineDto> lines)
    {
        lines = products.Distinct().Select(product => CreateLine(product, products.Count(p => p == product))).ToList();

        var shoppingCartDto = new ShoppingCartDto(lines, 0, lines.Sum(p => p.Cost*p.Qty));
        return shoppingCartDto;
    }


    private static LineDto CreateLine(Product product, int qty)
    {
        return new LineDto(product.Name, product.Cost, qty);
    }
}