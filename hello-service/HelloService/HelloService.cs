using System;

namespace Hello;

public class HelloService
{
    private readonly OutputService _outputService;
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly TimeSlot morning = new(new TimeOnly(6, 0), new TimeOnly(11, 59));
    private readonly TimeSlot afternoon = new(new TimeOnly(12, 0), new TimeOnly(19, 59));

    public HelloService(OutputService outputService, DateTimeProvider dateTimeProvider)
    {
        _outputService = outputService;
        _dateTimeProvider = dateTimeProvider;
    }

    public void Hello()
    {
        var time = _dateTimeProvider.Get();
        if (morning.IsInTime(time)) {
            _outputService.Write("Buenos dias!");
            return;
        }
        if (afternoon.IsInTime(time))
        {
            _outputService.Write("Buenas tardes!");
            return;
        }
        
        _outputService.Write("Buenas noches!");
    }
}