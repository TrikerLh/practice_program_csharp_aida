using System;

namespace LegacySecurityManager;

public class ConsoleInputRequester : InputRequester
{
    public string RequestInput(string requestMessage)
    {
        Console.WriteLine(requestMessage);
        return Console.ReadLine();
    }
}