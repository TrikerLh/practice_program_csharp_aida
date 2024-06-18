namespace Checkout;

public class Receipt
{
    private Receipt(Money amount, Money tax, Money total)
    {
        Amount = amount;
        Tax = tax;
        Total = total;
    }

    public static Receipt CreateReceipt(Money amount)
    {
        var vat = amount.Percentage(20);

        return new Receipt(amount, vat, amount.Add(vat));
    }

    public Money Amount { get; }
    public Money Tax { get; }
    public Money Total { get; }

    public IEnumerable<string> Format()
    {
        return new List<string>
        {
            //
            "Receipt", //
            "=======", //
            "Item 1 ... " + Amount.Format(), //
            "Tax    ... " + Tax.Format(), //
            "----------------", //
            "Total  ... " + Total.Format() //
        };
    }
}