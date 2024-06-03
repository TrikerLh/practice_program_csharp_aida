using System;

namespace LegacySecurityManager;

public class SecurityManager
{
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

        if (password != confirmPassword)
        {
            Print("The passwords don't match");
            return;
        }

        if (password.Length < 8)
        {
            Print("Password must be at least 8 characters in length");
            return;
        }

        // Encrypt the password (just reverse it, should be secure)
        char[] array = password.ToCharArray();
        Array.Reverse(array);

        Print($"Saving Details for User ({username}, {fullName}, {new string(array)})\n");
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