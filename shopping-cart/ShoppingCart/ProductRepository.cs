namespace ShoppingCart;

public interface ProductRepository
{
    Product Get(string productName);
}