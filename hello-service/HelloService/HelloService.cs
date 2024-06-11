using System;

namespace Hello;

public class HelloService
{
    private readonly OutputService _outputService;
    private readonly DateTimeProvider _dateTimeProvider;
    private readonly TimeSlot _morning; 
    private readonly TimeSlot _afternoon;

    public HelloService(OutputService outputService, DateTimeProvider dateTimeProvider)
    {
        _outputService = outputService;
        _dateTimeProvider = dateTimeProvider;
        _morning = new TimeSlot(new TimeOnly(6, 0), new TimeOnly(11, 59));
        _afternoon = new TimeSlot(new TimeOnly(12, 0), new TimeOnly(19, 59));
    }

    public void Hello()
    {
        var time = _dateTimeProvider.Get();
        if (_morning.IsInTime(time)) {
            _outputService.Write("Buenos dias!");
            return;
        }
        if (_afternoon.IsInTime(time))
        {
            _outputService.Write("Buenas tardes!");
            return;
        }
        
        _outputService.Write("Buenas noches!");
    }
}