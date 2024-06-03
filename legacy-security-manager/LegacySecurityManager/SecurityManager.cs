using System;

namespace LegacySecurityManager;

public class SecurityManager
{
    private const int MAX_PASSWORD_LENGTH = 8;

    public void CreateSecurityUser()
    {
        Print("Enter a username");
        var username = Read();
        Print("Enter your full name");
        var fullName = Read();
        Print("Enter your password");
        var password = Read();
        Print("Re-enter your password");
        var confirmPassword = Read();

        if (IsInvalidPassword(password, confirmPassword)) return;

        var array = EncryptPassword(password);

        Print($"Saving Details for User ({username}, {fullName}, {new string(array)})\n");
    }

    private char[] EncryptPassword(string password)
    {
        char[] array = password.ToCharArray();
        Array.Reverse(array);
        return array;
    }

    private bool IsInvalidPassword(string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            Print("The passwords don't match");
            return true;
        }

        if (password.Length < MAX_PASSWORD_LENGTH)
        {
            Print("Password must be at least 8 characters in length");
            return true;
        }

        return false;
    }

    protected virtual string Read()
    {
        return Console.ReadLine();
    }

    protected virtual void Print(string message)
    {
        Console.WriteLine(message);
    }

    public static void CreateUser()
    {
        new SecurityManager().CreateSecurityUser();
    }
}