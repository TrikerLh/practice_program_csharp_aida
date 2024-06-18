using NUnit.Framework;

namespace Checkout.Tests;

public class CheckoutTest
{
    [Test]
    public void CreateReceipt()
    {
        var checkout = new CheckoutForTest();

        var amount = new Money(12);
        var receitpt = checkout.CreateReceipt(amount);

        Assert.That(checkout, Is.Not.Null);
        Assert.That(amount, Is.EqualTo(receitpt.Amount));
        Assert.That(new Money(2.4m), Is.EqualTo(receitpt.Tax));
        Assert.That(new Money(14.4m), Is.EqualTo(receitpt.Total));
    }
}


public class CheckoutForTest : Checkout
{
    protected override void StoreReceipt(Receipt receipt)
    {
    }
}