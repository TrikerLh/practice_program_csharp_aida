namespace ShoppingCart;

public record Product(double Cost, string Name, double Revenue, double Tax)
{
    public double FinalCost()
    {
        var revenueAmount = (Cost * Revenue) / 100;
        var taxAmount = (Cost * Tax) / 100;
        return Cost + revenueAmount + taxAmount;
    }
}