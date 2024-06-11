using System;

namespace Hello;

public class TimeSlot
{
    private readonly TimeOnly _since;
    private readonly TimeOnly _until;

    public TimeSlot(TimeOnly since, TimeOnly until)
    {
        _since = since;
        _until = until;
    }

    public bool IsInTime(TimeOnly time)
    {
        return time.CompareTo(_since) >= 0 && time.CompareTo(_until) <= 0;
    }
}