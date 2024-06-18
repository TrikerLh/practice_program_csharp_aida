using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest
{
    [Test]
    public void CreateReceipt()
    {
        var checkout = new CheckoutForTest();

        var receipt = checkout.CreateReceipt(new Money(12));

        CheckReceipt(receipt, amount: 12, tax: 2.4m, total: 14.4m);
        Assert.That(1, Is.EqualTo(checkout.StoredReceipts.Count));
        CheckReceipt(checkout.StoredReceipts[0], amount: 12, tax: 2.4m, total: 14.4m);
    }

    private static void CheckReceipt(Receipt receipt, decimal amount, decimal tax, decimal total)
    {
        Assert.That(new Money(amount), Is.EqualTo(receipt.Amount));
        Assert.That(new Money(tax), Is.EqualTo(receipt.Tax));
        Assert.That(new Money(total), Is.EqualTo(receipt.Total));
    }
}


public class CheckoutForTest : Checkout
{
    public IList<Receipt> StoredReceipts { get; private set; }

    public CheckoutForTest()
    {
        StoredReceipts = new List<Receipt>();
    }

    protected override void StoreReceipt(Receipt receipt)
    {
        StoredReceipts.Add(receipt);
    }
}