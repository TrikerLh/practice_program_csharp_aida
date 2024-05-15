using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    public class ShoppingCartTest
    {
        [Test]
        public void Display_error_when_product_not_available()
        {
            var productRepository = Substitute.For<ProductRepository>();
            var discountRepository = Substitute.For<DiscountRepository>();
            var checkoutService = Substitute.For<CheckoutService>();
            var display = Substitute.For<Display>();
            var shoppingCart = new ShoppingCart(productRepository, discountRepository, checkoutService, display);
            var productNoAvailable = "IceCream";
            productRepository.Get(productNoAvailable).ReturnsNull();

            shoppingCart.AddItem(productNoAvailable);

            display.Received(1).Show("Producto no disponible.");
        }
    }

    public class ShoppingCart
    {
        private readonly ProductRepository _productRepository;
        private readonly DiscountRepository _discountRepository;
        private readonly CheckoutService _checkoutService;
        private readonly Display _display;

        public ShoppingCart(ProductRepository productRepository, DiscountRepository discountRepository, CheckoutService checkoutService, Display display)
        {
            _productRepository = productRepository;
            _discountRepository = discountRepository;
            _checkoutService = checkoutService;
            _display = display;
        }

        public void AddItem(string productName)
        {
               _display.Show("Producto no disponible.");
        }
    }

    public interface Display
    {
        void Show(string message);
    }

    public interface CheckoutService
    {
    }

    public interface DiscountRepository
    {
    }

    public interface ProductRepository
    {
        Product Get(string productName);
    }

    public class Product
    {
    }
}