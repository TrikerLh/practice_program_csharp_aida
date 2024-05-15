namespace ShoppingCart;

public record LineDto(string ProductName, double Cost, int Qty)
{
    public override string ToString()
    {
        return $"{nameof(ProductName)}: {ProductName}, {nameof(Cost)}: {Cost}, {nameof(Qty)}: {Qty}";
    }
}