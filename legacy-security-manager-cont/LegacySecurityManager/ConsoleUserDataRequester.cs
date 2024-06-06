namespace LegacySecurityManager;

public class ConsoleUserDataRequester : UserDataRequester
{
    private ConsoleInputRequester _consoleInputRequester;

    public ConsoleUserDataRequester(InputReader inputReader, Notifier notifier)
    {
        _consoleInputRequester = new ConsoleInputRequester(inputReader, notifier);
    }

    public UserData Request()
    {
        var username = RequestUserName();
        var fullName = RequestFullName();
        var password = RequestPassword();
        var confirmPassword = RequestPasswordConfirmation();
        return new UserData(username, fullName, password, confirmPassword);
    }

    private string RequestUserName()
    {
        return _consoleInputRequester.RequestInput("Enter a username");
    }

    private string RequestFullName()
    {
        return _consoleInputRequester.RequestInput("Enter your full name");
    }

    private string RequestPassword()
    {
        return _consoleInputRequester.RequestInput("Enter your password");
    }

    private string RequestPasswordConfirmation()
    {
        return _consoleInputRequester.RequestInput("Re-enter your password");
    }
}