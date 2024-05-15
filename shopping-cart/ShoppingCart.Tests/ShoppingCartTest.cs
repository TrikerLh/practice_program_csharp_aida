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
    }
}