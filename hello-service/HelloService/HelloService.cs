using System;

namespace Hello;

public class HelloService
{
    private readonly OutputService _outputService;
    private readonly TimeProvider _timeProvider;
    private readonly TimeSlot _morning; 
    private readonly TimeSlot _afternoon;

    public HelloService(OutputService outputService, TimeProvider timeProvider)
    {
        _outputService = outputService;
        _timeProvider = timeProvider;
        _morning = new TimeSlot(new TimeOnly(6, 0), new TimeOnly(11, 59));
        _afternoon = new TimeSlot(new TimeOnly(12, 0), new TimeOnly(19, 59));
    }

    public void Hello()
    {
        var time = _timeProvider.Get();
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