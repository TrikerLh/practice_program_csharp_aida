namespace ShoppingCart;

public class ReportLine
{
    public int Quantity { get; }
    public string Name { get; }
    public decimal TotalCost { get; }

    public ReportLine(string name, int quantity, decimal totalCost)
    {
        Name = name;
        Quantity = quantity;
        TotalCost = totalCost;
    }
}
