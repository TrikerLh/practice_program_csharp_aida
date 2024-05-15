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
    private Discount _discount;

    public ShoppingCart(ProductRepository productRepository, DiscountRepository discountRepository, CheckoutService checkoutService, Display display)
    {
        _productRepository = productRepository;
        _discountRepository = discountRepository;
        _checkoutService = checkoutService;
        _display = display;
        products = new List<Product>();
        _discount = new Discount("PROMO_0", 0.0);
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

        var shoppingCartDto = new ShoppingCartDto(lines, _discount.Amount, lines.Sum(line => CalculateTotalAmount(line, _discount.Amount)));
        return shoppingCartDto;
    }

    private static double CalculateTotalAmount(LineDto line, double amount)
    {
        var discountAmount = (line.Cost * amount) / 100;
        return line.Cost*line.Qty - discountAmount;
    }


    private static LineDto CreateLine(Product product, int qty)
    {
        return new LineDto(product.Name, product.FinalCost(), qty);
    }

    public void ApplyDiscount(string discountCode)
    {
        var discount = _discountRepository.Get(discountCode);
        if (discount == null)
        {
            _display.Show("Descuento no disponible.");
        }

        _discount = discount;
    }
}