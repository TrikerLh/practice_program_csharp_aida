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
        _outputService.Write("Buenos dias!");
    }
}