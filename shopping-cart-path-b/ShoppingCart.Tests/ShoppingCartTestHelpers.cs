using System.Globalization;

namespace ShoppingCart.Tests
{
    public class ShoppingCartTestHelpers
    {
        public static CheckoutDto CreateShoppingCartDto(decimal cost)
        {
            return new CheckoutDto(cost);
        }

        public static ShoppingCart CreateShoppingCartForCheckout(
            ProductsRepository productsRepository,
            Notifier notifier,
            CheckoutService checkoutService,
            DiscountsRepository discountsRepository)
        {
            return new ShoppingCart(productsRepository, notifier, null, checkoutService, discountsRepository, new TextReportFormatter(new CultureInfo("en-GB")));
        }

        public static ShoppingCart CreateShoppingCartForDisplay(ProductsRepository productsRepository,
            Notifier notifier,
            Display display,
            DiscountsRepository discountsRepository, TextReportFormatter reportFormatter)
        {
            return new ShoppingCart(productsRepository, notifier, display, null, discountsRepository, reportFormatter);
        }
    }
}