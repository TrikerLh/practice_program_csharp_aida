namespace ShoppingCart;

public interface DiscountRepository
{
    Discount Get(string discountCode);
}