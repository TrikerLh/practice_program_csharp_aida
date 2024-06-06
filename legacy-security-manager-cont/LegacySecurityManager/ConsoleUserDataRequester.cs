namespace LegacySecurityManager;

public class ConsoleUserDataRequester : UserDataRequester
{
    private InputRequester _inputRequester;

    public ConsoleUserDataRequester(InputRequester inputRequester)
    {
        _inputRequester = inputRequester;
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
        return _inputRequester.RequestInput("Enter a username");
    }

    private string RequestFullName()
    {
        return _inputRequester.RequestInput("Enter your full name");
    }

    private string RequestPassword()
    {
        return _inputRequester.RequestInput("Enter your password");
    }

    private string RequestPasswordConfirmation()
    {
        return _inputRequester.RequestInput("Re-enter your password");
    }
}