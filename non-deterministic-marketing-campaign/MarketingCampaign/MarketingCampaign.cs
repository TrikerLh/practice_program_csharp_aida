namespace MarketingCampaign;

public class MarketingCampaign
{
    public bool IsActive()
    {
        return MilliSeconds() % 2 == 0;
    }

    public bool IsCrazySalesDay()
    {
        return DayOfTheWeek().Equals(DayOfWeek.Friday);
    }

    private long MilliSeconds()
    {
        return (long)GetDateTime().TimeOfDay.TotalMilliseconds;
    }

    private DayOfWeek DayOfTheWeek()
    {
        return GetDateTime().DayOfWeek;
    }

    protected virtual DateTime GetDateTime()
    {
        return DateTime.Now;
    }
}