using System;

namespace LegacySecurityManager.Ciphers;

public class ReverseCipher : Cipher
{
    public string Encrypt(string str)
    {
        var array = str.ToCharArray();
        Array.Reverse((Array)array);
        return new string(array);
    }
}