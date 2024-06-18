namespace Checkout;

public class Checkout
{
    private const int TaxPercentage = 20;

    public Receipt CreateReceipt(Money amount)
    {
        var receipt = Receipt.CreateReceipt(amount, TaxPercentage);

        StoreReceipt(receipt);

        return receipt;
    }

    protected virtual void StoreReceipt(Receipt receipt)
    {
        ReceiptRepository.Store(receipt);
    }
}