using System.Collections.Generic;

namespace ShoppingCart;

public class ShoppingCart
{
    private readonly ProductsRepository _productsRepository;
    private readonly Notifier _notifier;
    private readonly Display _display;
    private readonly CheckoutService _checkoutService;
    private readonly DiscountsRepository _discountsRepository;
    private ProductList _productList;
    private TextReportFormatter _textReportFormatter;

    public ShoppingCart(ProductsRepository productsRepository,
        Notifier notifier,
        Display display,
        CheckoutService checkoutService,
        DiscountsRepository discountsRepository,
        TextReportFormatter textReportFormatter) {
        _productsRepository = productsRepository;
        _notifier = notifier;
        _display = display;
        _checkoutService = checkoutService;
        _discountsRepository = discountsRepository;
        _textReportFormatter = textReportFormatter;

        InitializeState();
    }

    public void AddItem(string productName)
    {
        var product = _productsRepository.Get(productName);
        if (product is null)
        {
            _notifier.ShowError("Product is not available");
            return;
        }

        _productList.AddProduct(product);

    }

    public void ApplyDiscount(DiscountCode discountCode)
    {
        var discount = _discountsRepository.Get(discountCode);
        if (discount is null)
        {
            _notifier.ShowError("Discount is not available");
            return;
        }

        _productList.AddDiscount(discount);
    }

    public void Checkout()
    {
        if (_productList.ThereAreNoProducts())
        {
            NotifyEmptyShoppingCart();
            return;
        }
        PerformCheckout();
        InitializeState();
    }

    private void InitializeState()
    {
        new List<Product>();
        _productList = new ProductList();
    }

    private void NotifyEmptyShoppingCart()
    {
        _notifier.ShowError("No product selected, please select a product");
    }

    private void PerformCheckout()
    {
        var totalCost = _productList.ComputeTotalCost();
        var shoppingCartDto = new CheckoutDto(totalCost);
        _checkoutService.Checkout(shoppingCartDto);
    }

    public void Display()
    {
        _display.Show(_textReportFormatter.Format(new GroupedReport(_productList)));
    }
}