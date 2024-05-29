namespace ShoppingCart;

public class Discount
{
    public readonly DiscountCode DiscountCode;
    public readonly decimal Amount;

    public Discount(DiscountCode discountCode, decimal amount)
    {
        DiscountCode = discountCode;
        Amount = amount;
    }

    public decimal Apply(decimal totalCost)
    {
        return totalCost * (1 - Amount);
    }

    public bool HasDiscount()
    {
        return DiscountCode != DiscountCode.None;
    }
}