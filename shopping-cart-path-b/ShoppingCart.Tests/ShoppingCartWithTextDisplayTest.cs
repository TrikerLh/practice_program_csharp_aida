using System.Globalization;
using NSubstitute;
using NUnit.Framework;
using static ShoppingCart.Tests.ProductBuilder;
using static ShoppingCart.Tests.ShoppingCartTestHelpers;

namespace ShoppingCart.Tests;

public class ShoppingCartWithTextDisplayTest
{
    private const string Iceberg = "Iceberg";
    private const string Tomato = "Tomato";
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
        _shoppingCart = CreateShoppingCartForGreatBritainDisplay();
    }

    private ShoppingCart CreateShoppingCartForGreatBritainDisplay() {
        var cultureInfo = new CultureInfo("en-GB");
        return CreateShoppingCartForDisplayWithCultureInfo(cultureInfo);
    }

    private ShoppingCart CreateShoppingCartForSpainDisplay() {
        var cultureInfo = new CultureInfo("es-ES");
        return CreateShoppingCartForDisplayWithCultureInfo(cultureInfo);
    }

    private ShoppingCart CreateShoppingCartForDisplayWithCultureInfo(CultureInfo cultureInfo) {
        return CreateShoppingCartForDisplay(_productsRepository, _notifier, _display, _discountsRepository, new TextReportFormatter(cultureInfo));
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

    [Test]
    public void with_two_products_of_the_same() {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(1m).Build());
        _shoppingCart.AddItem(Iceberg);
        _shoppingCart.AddItem(Iceberg);

        _shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 2€, 2\nTotal products: 2\nTotal price: 2€");
    }

    [Test]
    public void with_two_different_products() {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(1m).Build());
        _productsRepository.Get(Tomato).Returns(
            TaxFreeWithNoRevenueProduct().Named(Tomato).Costing(2m).Build());
        _shoppingCart.AddItem(Iceberg);
        _shoppingCart.AddItem(Tomato);
    
        _shoppingCart.Display();
            
        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 1€, 1\nTomato, 2€, 1\nTotal products: 2\nTotal price: 3€");
    }

    [Test]
    public void with_one_product_with_discount()
    {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(10m).Build());
        _discountsRepository.Get(DiscountCode.PROMO_10).Returns(
        new Discount(DiscountCode.PROMO_10, 0));
        _shoppingCart.AddItem(Iceberg);
        _shoppingCart.ApplyDiscount(DiscountCode.PROMO_10);

        _shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 10€, 1\nPromotion: 0% off with code PROMO_10\nTotal products: 1\nTotal price: 10€");
    }

    [Test]
    public void with_decimal_amount_for_great_britain_culture() {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(2.5m).Build());
        _shoppingCart.AddItem(Iceberg);

        _shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 2.5€, 1\nTotal products: 1\nTotal price: 2.5€");

    }

    [Test]
    public void with_decimal_amount_for_spanish_culture() {
        var shoppingCart = CreateShoppingCartForSpainDisplay();
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(2.5m).Build());
        shoppingCart.AddItem(Iceberg);

        shoppingCart.Display();

        _display.Received(1).Show("Product name, Price with VAT, Quantity\nIceberg, 2,5€, 1\nTotal products: 1\nTotal price: 2,5€");

    }

}