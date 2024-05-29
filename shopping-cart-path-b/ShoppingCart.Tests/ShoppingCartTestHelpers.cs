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
            return new ShoppingCart(productsRepository, notifier, null, checkoutService, discountsRepository);
        }

        public static ShoppingCart CreateShoppingCartForDisplay(ProductsRepository productsRepository,
            Notifier notifier,
            Display display,
            DiscountsRepository discountsRepository)
        {
            return new ShoppingCart(productsRepository, notifier, display, null, discountsRepository);
        }
    }
}