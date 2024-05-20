namespace ShoppingCart;

public static class ShoppingCartSummary
{
    public static string Summary()
    {
        return $"{CreateHeader()}\n{CreateFooter()}";
    }

    private static string CreateFooter()
    {
        return "Total products: 0\nTotal price: 0\u20ac";
    }

    private static string CreateHeader()
    {
        return "Product name, Price with VAT, Quantity";
    }
}