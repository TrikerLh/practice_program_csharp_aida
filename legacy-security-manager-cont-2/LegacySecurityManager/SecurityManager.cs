using LegacySecurityManager.infrastructure;
using System;

namespace LegacySecurityManager;



public class SecurityManager
{
    private readonly Notifier _notifier;
    private readonly ConsoleUserDataRequester _userDataRequester;

    public SecurityManager(Notifier notifier, Input input)
    {
        _notifier = notifier;
        _userDataRequester = new ConsoleUserDataRequester(input);
    }

    public void CreateValidUser()
    {
        var userData = _userDataRequester.Request();

        if (PasswordsDoNotMatch(userData.Password(), userData.ConfirmPassword()))
        {
            NotifyPasswordDoNotMatch();
            return;
        }

        if (IsPasswordToShort(userData.Password()))
        {
            NotifyPasswordIsToShort();
            return;
        }

        var encryptedPassword = EncryptPassword(userData.Password());
        NotifyUserCreation(userData.UserName(), userData.FullName(), encryptedPassword);
    }

    private void NotifyPasswordIsToShort()
    {
        Print("Password must be at least 8 characters in length");
    }

    private void NotifyPasswordDoNotMatch()
    {
        Print("The passwords don't match");
    }

    private void NotifyUserCreation(string username, string fullName, string encryptedPassword)
    {
        Print($"Saving Details for User ({username}, {fullName}, {encryptedPassword})\n");
    }

    private static string EncryptPassword(string password)
    {
        var array = password.ToCharArray();
        Array.Reverse(array);
        var encryptedPassword = new string(array);
        return encryptedPassword;
    }

    private static bool IsPasswordToShort(string password)
    {
        return password.Length < 8;
    }

    private static bool PasswordsDoNotMatch(string password, string confirmPassword)
    {
        return password != confirmPassword;
    }

    private void Print(string message)
    {
        _notifier.Notify(message);
    }

    public static void CreateUser()
    {
        Notifier notifier = new ConsoleNotifier();
        new SecurityManager(notifier, new ConsoleInput()).CreateValidUser();
    }

    internal class ConsoleUserDataRequester {
        private Input input;

        public ConsoleUserDataRequester(Input input) {
            this.input = input;
        }
        public UserData Request() {

            return new UserData(RequestUserName(), RequestFullName(), RequestPassword(), RequestPasswordConfirmation());
        }

        private string RequestPasswordConfirmation() {
            return input.Request("Re-enter your password");
        }

        private string RequestPassword() {
            return input.Request("Enter your password");
        }

        private string RequestFullName() {
            return input.Request("Enter your full name");
        }

        private string RequestUserName() {
            return input.Request("Enter a username");
        }

    }
}



