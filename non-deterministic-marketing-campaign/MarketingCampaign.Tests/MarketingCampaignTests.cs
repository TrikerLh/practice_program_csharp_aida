using System.Linq.Expressions;
using NUnit.Framework;

namespace MarketingCampaign.Tests;

public class MarketingCampaignTests
{
    [Test]
    public void Is_Crazy_Sales_Day()
    {
        var friday = new DateTime(2024,06, 14, 00, 00, 00);
        var campaign = new MarketingCampaignForTest(friday);

        var isCrazySalesDay = campaign.IsCrazySalesDay();

        Assert.That(isCrazySalesDay, Is.True);
    }

    [Test]
    public void MarketingCampaign_Is_Active()
    {
        var dateTimePairMilliseconds = new DateTime(2024, 06, 14, 00, 00, 00).AddMilliseconds(24);
        var campaign = new MarketingCampaignForTest(dateTimePairMilliseconds);

        var isActive = campaign.IsActive();

        Assert.That(isActive, Is.True);
    }

    [Test]
    public void Not_Is_Crazy_Sales_Day() {
        var notIsFriday = new DateTime(2024, 06, 15, 00, 00, 00);
        var campaign = new MarketingCampaignForTest(notIsFriday);

        var isCrazySalesDay = campaign.IsCrazySalesDay();

        Assert.That(isCrazySalesDay, Is.False);
    }

    [Test]
    public void MarketingCampaign_Is_Not_Active() {
        var dateTimeOddsMilliseconds = new DateTime(2024, 06, 14, 00, 00, 42).AddMilliseconds(1);
        var campaign = new MarketingCampaignForTest(dateTimeOddsMilliseconds);

        var isActive = campaign.IsActive();

        Assert.That(isActive, Is.False);
    }
}

public class MarketingCampaignForTest : MarketingCampaign
{
    private readonly DateTime _dateTime;

    public MarketingCampaignForTest(DateTime dateTime)
    {
        _dateTime = dateTime;
    }
    protected override DateTime GetDateTime()
    {
        return _dateTime;
    }
}