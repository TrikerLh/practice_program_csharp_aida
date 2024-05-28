using System.Collections.Generic;

namespace ShoppingCart;

public class ShoppingCart
{
    private readonly ProductsRepository _productsRepository;
    private readonly Notifier _notifier;
    private readonly Display _display;
    private readonly CheckoutService _checkoutService;
    private readonly DiscountsRepository _discountsRepository;
    private List<Product> _productList;
    private Discount _discount;
    private ProductList _productListRefactor;

    public ShoppingCart(ProductsRepository productsRepository,
        Notifier notifier,
        Display display,
        CheckoutService checkoutService,
        DiscountsRepository discountsRepository)
    {
        _productsRepository = productsRepository;
        _notifier = notifier;
        _display = display;
        _checkoutService = checkoutService;
        _discountsRepository = discountsRepository;
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

        _productListRefactor.AddProduct(product);

    }

    public void ApplyDiscount(DiscountCode discountCode)
    {
        var discount = _discountsRepository.Get(discountCode);
        if (discount is null)
        {
            _notifier.ShowError("Discount is not available");
            return;
        }
        _discount = discount;
        _productListRefactor.AddDiscount(discount);
    }

    public void Checkout()
    {
        if (_productListRefactor.ThereAreNoProducts())
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
        _productListRefactor = new ProductList(_productList);
    }

    private void NotifyEmptyShoppingCart()
    {
        _notifier.ShowError("No product selected, please select a product");
    }

    private void PerformCheckout()
    {
        var totalCost = _productListRefactor.ComputeTotalCost();
        var shoppingCartDto = new ShoppingCartDto(totalCost);
        _checkoutService.Checkout(shoppingCartDto);
    }

    public void Display()
    {
        var formatter = new ShoppingCartSummaryFormatter(_productList, _productListRefactor.ComputeTotalCost());
        _display.Show(formatter.Format());
    }
}