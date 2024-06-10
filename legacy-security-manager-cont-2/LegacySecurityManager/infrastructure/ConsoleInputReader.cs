using System;

namespace LegacySecurityManager.infrastructure;

public class ConsoleInputReader : InputReader {
    public string Read(string requestMessage)
    {
        Console.WriteLine(requestMessage);
        return Console.ReadLine();
    }
}