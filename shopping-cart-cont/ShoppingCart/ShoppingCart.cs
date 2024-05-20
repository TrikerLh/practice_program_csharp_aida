using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public class ShoppingCart
{
    private readonly Display _display;
    private readonly ProductsRepository _productsRepository;
    private readonly ErrorNotifier _errorNotifier;
    private readonly CheckoutService _checkoutService;
    private readonly DiscountsRepository _discountsRepository;
    private List<Product> _productList;
    private Discount _discount;

    public ShoppingCart(ProductsRepository productsRepository, ErrorNotifier errorNotifier, CheckoutService checkoutService, DiscountsRepository discountsRepository, Display display) {
        _display = display;
        _productsRepository = productsRepository;
        _errorNotifier = errorNotifier;
        _checkoutService = checkoutService;
        _discountsRepository = discountsRepository;
        InitializeState();
    }

    public void AddItem(string productName)
    {
        var product = _productsRepository.Get(productName);
        if (product is null)
        {
            _errorNotifier.ShowError("Product is not available");
            return;
        }
        _productList.Add(product);

    }

    public void ApplyDiscount(DiscountCode discountCode)
    {
        var discount = _discountsRepository.Get(discountCode);
        if (discount is null)
        {
            _errorNotifier.ShowError("Discount is not available");
            return;
        }
        _discount = discount;
    }

    public void Checkout()
    {
        if (ThereAreNoProducts())
        {
            NotifyEmptyShoppingCart();
            return;
        }
        PerformCheckout();
        InitializeState();
    }

    private void InitializeState()
    {
        _productList = new List<Product>();
        _discount = new Discount(DiscountCode.None, 0);
    }

    private void NotifyEmptyShoppingCart()
    {
        _errorNotifier.ShowError("No product selected, please select a product");
    }

    private void PerformCheckout()
    {
        var totalCost = ComputeTotalCost();
        var shoppingCartDto = new ShoppingCartDto(totalCost);
        _checkoutService.Checkout(shoppingCartDto);
    }

    private bool ThereAreNoProducts()
    {
        return !_productList.Any();
    }

    private decimal ComputeTotalCost()
    {
        var totalCost = ComputeAllProductsCost();
        return _discount.Apply(totalCost);
    }

    private decimal ComputeAllProductsCost()
    {
        return _productList.Sum(p => p.ComputeCost());
    }

    public void Display() {
        _display.Show("Product name, Price with VAT, Quantity\nTotal products: 0\nTotal price: 0€");
    }
}