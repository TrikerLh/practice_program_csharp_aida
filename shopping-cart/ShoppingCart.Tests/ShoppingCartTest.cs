using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    public class ShoppingCartTest
    {
        private ProductRepository _productRepository;
        private DiscountRepository _discountRepository;
        private CheckoutService _checkoutService;
        private Display _display;
        private ShoppingCart _shoppingCart;
        private const string AnyProduct = "Iceberg";

        [SetUp]
        public void Setup()
        {
            _productRepository = Substitute.For<ProductRepository>();
            _discountRepository = Substitute.For<DiscountRepository>();
            _checkoutService = Substitute.For<CheckoutService>();
            _display = Substitute.For<Display>();
            _shoppingCart = new ShoppingCart(_productRepository, _discountRepository, _checkoutService, _display);
        }

        [Test]
        public void Display_error_when_product_not_available()
        {
            var productNoAvailable = "IceCream";
            _productRepository.Get(productNoAvailable).ReturnsNull();

            _shoppingCart.AddItem(productNoAvailable);

            _display.Received(1).Show("Producto no disponible.");
        }

        [Test]
        public void checkout_a_product_should_calculate_total_amount()
        {
            var otherProduct = "Tomatoe";
            _productRepository.Get(AnyProduct).Returns(new Product(Name: AnyProduct, Cost: 1.0, Revenue: 0.0, Tax:0.0 ));
            _productRepository.Get(otherProduct).Returns(new Product(Name: otherProduct, Cost: 2.0, Revenue: 0.0, Tax:0.0 ));
            _shoppingCart.AddItem(AnyProduct);
            _shoppingCart.AddItem(otherProduct);

            _shoppingCart.Checkout();

            var lines = new List<LineDto>() { new(ProductName: AnyProduct, Cost: 1.0, Qty: 1), new(ProductName: otherProduct, Cost: 2.0, Qty: 1) };
            _checkoutService.Received(1).Checkout(Arg.Is<ShoppingCartDto>(s => validate(s, new ShoppingCartDto(lines, 0.0, 3.0))) );
        }

        [Test]
        public void checkout_a_product_should_calculate_quantity_of_products()
        {
            _productRepository.Get(AnyProduct).Returns(new Product(Name: AnyProduct, Cost: 1.0, Revenue: 0.0, Tax: 0.0));
            _shoppingCart.AddItem(AnyProduct);
            _shoppingCart.AddItem(AnyProduct);

            _shoppingCart.Checkout();

            var lines = new List<LineDto>() { new(ProductName: AnyProduct, Cost: 1.0, Qty: 2)};
            _checkoutService.Received(1).Checkout(Arg.Is<ShoppingCartDto>(s => validate(s, new ShoppingCartDto(lines, 0.0, 2.0))));
        }

        [Test]
        public void checkout_a_product_with_revenue()
        {
            _productRepository.Get(AnyProduct).Returns(new Product(Name: AnyProduct, Cost: 100.0, Revenue: 10.0, Tax: 0.0));
            _shoppingCart.AddItem(AnyProduct);

            _shoppingCart.Checkout();

            var lines = new List<LineDto>() { new(ProductName: AnyProduct, Cost: 110.0, Qty: 1) };
            _checkoutService.Received(1).Checkout(Arg.Is<ShoppingCartDto>(s => validate(s, new ShoppingCartDto(lines, 0.0, 110.0))));
        }

        [Test]
        public void checkout_a_product_with_tax() {
            _productRepository.Get(AnyProduct).Returns(new Product(Name: AnyProduct, Cost: 100.0, Revenue: 0.0, Tax: 10.0));
            _shoppingCart.AddItem(AnyProduct);

            _shoppingCart.Checkout();

            var lines = new List<LineDto>() { new(ProductName: AnyProduct, Cost: 110.0, Qty: 1) };
            _checkoutService.Received(1).Checkout(Arg.Is<ShoppingCartDto>(s => validate(s, new ShoppingCartDto(lines, 0.0, 110.0))));
        }

        [Test]
        public void Display_error_when_discount_not_available() {
            var discountNoAvailable = "PROMO_NAVIDAD";
            _discountRepository.Get(discountNoAvailable).ReturnsNull();

            _shoppingCart.ApplyDiscount(discountNoAvailable);

            _display.Received(1).Show("Descuento no disponible.");
        }

        //[Test]
        //public void checkout_a_product_with_discount() {
        //    _productRepository.Get(AnyProduct).Returns(new Product(Name: AnyProduct, Cost: 100.0, Revenue: 0.0, Tax: 0.0));
        //    _shoppingCart.AddItem(AnyProduct);
        //    _discountRepository.Get("PROMO_10").Return()

        //    _shoppingCart.Checkout();

        //    var lines = new List<LineDto>() { new(ProductName: AnyProduct, Cost: 110.0, Qty: 1) };
        //    _checkoutService.Received(1).Checkout(Arg.Is<ShoppingCartDto>(s => validate(s, new ShoppingCartDto(lines, 0.0, 110.0))));
        //}

        private bool validate(ShoppingCartDto shoppingCartDto, ShoppingCartDto shoppingCartDto1)
        {
            Assert.That(shoppingCartDto.Lines, Is.EquivalentTo(shoppingCartDto1.Lines));
            Assert.That(shoppingCartDto.Discount, Is.EqualTo(shoppingCartDto1.Discount));
            Assert.That(shoppingCartDto.TotalAmount, Is.EqualTo(shoppingCartDto1.TotalAmount));

            return true;
        }
    }
}