using NSubstitute;
using NUnit.Framework;

namespace ShoppingCart.Tests;

public class ShoppingCartTest
{
    protected ProductsRepository _productsRepository;
    protected ErrorNotifier _errorNotifier;
    protected ShoppingCartCheckout _shoppingCartCheckout;
    protected CheckoutService _checkoutService;
    protected DiscountsRepository _discountsRepository;
    protected Display _display;

    [SetUp]
    public void SetUp()
    {
        _productsRepository = Substitute.For<ProductsRepository>();
        _errorNotifier = Substitute.For<ErrorNotifier>();
        _checkoutService = Substitute.For<CheckoutService>();
        _discountsRepository = Substitute.For<DiscountsRepository>();
        _display = Substitute.For<Display>();
        _shoppingCartCheckout = new ShoppingCartCheckout(_productsRepository, _errorNotifier, _checkoutService, _discountsRepository, _display);
    }
}