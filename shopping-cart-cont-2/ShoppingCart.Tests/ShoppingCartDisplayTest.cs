using NSubstitute;
using NUnit.Framework;
using static ShoppingCart.Tests.ProductBuilder;
using static ShoppingCart.Tests.ShoppingCartTestHelpers;

namespace ShoppingCart.Tests;

public class ShoppingCartDisplayTest
{
    private const string Iceberg = "Iceberg";
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

    [Test]
    public void with_one_product()
    {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(1m).Build());
        _shoppingCart.AddItem(Iceberg);

        _shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 1€, 1\nTotal products: 1\nTotal price: 1€");
    }
}