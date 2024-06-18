using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest
{
    [Test]
    public void CreateReceipt()
    {
        var checkout = new CheckoutForTest();

        var amount = new Money(12);
        var receipt = checkout.CreateReceipt(amount);

        Assert.That(amount, Is.EqualTo(receipt.Amount));
        Assert.That(new Money(2.4m), Is.EqualTo(receipt.Tax));
        Assert.That(new Money(14.4m), Is.EqualTo(receipt.Total));

        Assert.That(1, Is.EqualTo(checkout.StoredReceipts.Count));
        Assert.That(amount, Is.EqualTo(checkout.StoredReceipts[0].Amount));
        Assert.That(new Money(2.4m), Is.EqualTo(checkout.StoredReceipts[0].Tax));
        Assert.That(new Money(14.4m), Is.EqualTo(checkout.StoredReceipts[0].Total));
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