namespace LegacySecurityManager;

public class ConsoleInputRequester : InputRequester
{
    private readonly InputReader _inputReader;
    private readonly Notifier _notifier;

    public ConsoleInputRequester(InputReader inputReader, Notifier notifier)
    {
        _inputReader = inputReader;
        _notifier = notifier;
    }

    public string RequestInput(string requestMessage)
    {
        _notifier.Notify(requestMessage);
        return _inputReader.Read();
    }
}