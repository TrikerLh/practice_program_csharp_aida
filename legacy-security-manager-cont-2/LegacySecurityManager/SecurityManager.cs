using LegacySecurityManager.infrastructure;

namespace LegacySecurityManager;

public class SecurityManager
{
    private readonly Notifier _notifier;
    private readonly ConsoleUserInputRequester _userInputRequester;

    public SecurityManager(Notifier notifier, InputReader inputReader)
    {
        _notifier = notifier;
        _userInputRequester = new ConsoleUserInputRequester(inputReader);
    }

    public void CreateValidUser()
    {
        var userInput = _userInputRequester.Request();

        if (userInput.PasswordsDoNotMatch())
        {
            NotifyPasswordDoNotMatch();
            return;
        }

        if (userInput.IsPasswordToShort())
        {
            NotifyPasswordIsToShort();
            return;
        }

        var encryptedPassword = userInput.EncryptPassword();
        NotifyUserCreation(encryptedPassword, userInput);
    }

    private void NotifyPasswordIsToShort()
    {
        Print("Password must be at least 8 characters in length");
    }

    private void NotifyPasswordDoNotMatch()
    {
        Print("The passwords don't match");
    }

    private void NotifyUserCreation(string encryptedPassword, UserInput userInput)
    {
        Print($"Saving Details for User ({userInput.UserName()}, {userInput.FullName()}, {encryptedPassword})\n");
    }

    private void Print(string message)
    {
        _notifier.Notify(message);
    }

    public static void CreateUser()
    {
        Notifier notifier = new ConsoleNotifier();
        new SecurityManager(notifier, new ConsoleInputReader()).CreateValidUser();
    }

    internal class ConsoleUserInputRequester {
        private InputReader inputReader;

        public ConsoleUserInputRequester(InputReader inputReader) {
            this.inputReader = inputReader;
        }
        public UserInput Request() {

            return new UserInput(RequestUserName(), RequestFullName(), RequestPassword(), RequestPasswordConfirmation());
        }

        private string RequestPasswordConfirmation() {
            return inputReader.Read("Re-enter your password");
        }

        private string RequestPassword() {
            return inputReader.Read("Enter your password");
        }

        private string RequestFullName() {
            return inputReader.Read("Enter your full name");
        }

        private string RequestUserName() {
            return inputReader.Read("Enter a username");
        }

    }
}



