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
        if (time.CompareTo(new TimeOnly(6, 0)) >= 0 && time.CompareTo(new TimeOnly(11, 59)) <= 0)
        {
            _outputService.Write("Buenos dias!");
        }
        _outputService.Write("Buenas tardes!");

    }
}