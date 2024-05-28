namespace ShoppingCart;

public class ShoppingCartReportItem
{
    public int Quantity { get; }
    public string Name { get; }
    public decimal TotalCost { get; }

    public ShoppingCartReportItem(string name, int quantity, decimal totalCost)
    {
        Name = name;
        Quantity = quantity;
        TotalCost = totalCost;
    }
}
