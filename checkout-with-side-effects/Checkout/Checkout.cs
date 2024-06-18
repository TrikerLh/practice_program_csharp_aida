namespace Checkout;

public class Checkout
{
    public Receipt CreateReceipt(Money amount)
    {
        var vat = amount.Percentage(20);

        var receipt = Receipt.CreateReceipt(amount);

        StoreReceipt(receipt);

        return receipt;
    }

    protected virtual void StoreReceipt(Receipt receipt)
    {
        ReceiptRepository.Store(receipt);
    }
}