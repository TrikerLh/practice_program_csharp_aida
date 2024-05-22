using NSubstitute;
using NUnit.Framework;
using static ShoppingCart.Tests.ShoppingCartTestHelpers;

namespace ShoppingCart.Tests;

public class ShoppingCartDisplayTest
{
    private ProductsRepository _productsRepository;
    private Notifier _notifier;
    private DiscountsRepository _discountsRepository;
    private Display _display;

    private ShoppingCart _shoppingCart;

    [SetUp]
    public void SetUp()
    {
        _productsRepository = Substitute.For<ProductsRepository>();
        _notifier = Substitute.For<Notifier>();
        _discountsRepository = Substitute.For<DiscountsRepository>();
        _display = Substitute.For<Display>();

        _shoppingCart = CreateShoppingCartForDisplay(_productsRepository, _notifier, _display, _discountsRepository);
    }

    [Test]
    public void without_products()
    {
        _shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nTotal products: 0\nTotal price: 0€");
    }
}