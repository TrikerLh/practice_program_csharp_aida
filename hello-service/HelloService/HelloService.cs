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
            return;
        }
        if (IsBetween(time, new TimeOnly(12, 0),new TimeOnly(19, 59)))
        {
            _outputService.Write("Buenas tardes!");
            return;
        }
        
        _outputService.Write("Buenas noches!");
    }

    private bool IsBetween(TimeOnly time, TimeOnly since, TimeOnly until)
    {
        return time.CompareTo(since) >= 0 && time.CompareTo(until) <= 0;
    }
}