namespace LegacySecurityManager;

public interface UserDataRequester
{
    UserData Request();
}

public class ConsoleUserDataRequester : UserDataRequester
{
    private readonly InputReader _inputReader;
    private readonly Notifier _notifier;

    public ConsoleUserDataRequester(InputReader inputReader, Notifier notifier)
    {
        _inputReader = inputReader;
        _notifier = notifier;
    }

    public UserData Request()
    {
        var username = RequestUserName();
        var fullName = RequestFullName();
        var password = RequestPassword();
        var confirmPassword = RequestPasswordConfirmation();
        return new UserData(username, fullName, password, confirmPassword);
    }

    private string ReadUserInput()
    {
        return _inputReader.Read();
    }

    private string RequestUserInput(string requestMessage)
    {
        _notifier.Notify(requestMessage);
        return ReadUserInput();
    }

    private string RequestUserName()
    {
        return RequestUserInput("Enter a username");
    }

    private string RequestFullName()
    {
        return RequestUserInput("Enter your full name");
    }

    private string RequestPassword()
    {
        return RequestUserInput("Enter your password");
    }

    private string RequestPasswordConfirmation()
    {
        return RequestUserInput("Re-enter your password");
    }
}