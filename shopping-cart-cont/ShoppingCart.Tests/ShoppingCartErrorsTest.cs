using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NSubstitute;

namespace ShoppingCart.Tests
{
    public class ShoppingCartErrorsTest : ShoppingCartTest
    {
        [Test]
        public void add_not_available_product()
        {
            const string notAvailableProductName = "some_item";
            _productsRepository.Get(notAvailableProductName).ReturnsNull();

            _shoppingCart.AddItem(notAvailableProductName);

            _errorNotifier.Received(1).ShowError("Product is not available");
        }

        [Test]
        public void apply_not_available_discount()
        {
            var notAvailableDiscount = DiscountCode.PROMO_20;
            _discountsRepository.Get(notAvailableDiscount).ReturnsNull();

            _shoppingCart.ApplyDiscount(notAvailableDiscount);

            _errorNotifier.Received(1).ShowError("Discount is not available");
        }
    }
}
