using System;

namespace Hello;

public class HelloService
{
    private readonly OutputService _outputService;
    private readonly DateTimeProvider _dateTimeProvider;

    public HelloService(OutputService outputService, DateTimeProvider dateTimeProvider)
    {
        _outputService = outputService;
        _dateTimeProvider = dateTimeProvider;
    }

    public void Hello()
    {
        var time = _dateTimeProvider.Get();
        if (IsBetween(time, new TimeOnly(6, 0), new TimeOnly(11, 59)))
        {
            _outputService.Write("Buenos dias!");
        }
        else if (IsBetween(time, new TimeOnly(12, 0),new TimeOnly(19, 59)))
        {
            _outputService.Write("Buenas tardes!");
        }
        else 
        {
            _outputService.Write("Buenas noches!");
        }
        

    }

    private static bool IsBetween(TimeOnly time, TimeOnly since, TimeOnly until)
    {
        return time.CompareTo(since) >= 0 && time.CompareTo(until) <= 0;
    }
}