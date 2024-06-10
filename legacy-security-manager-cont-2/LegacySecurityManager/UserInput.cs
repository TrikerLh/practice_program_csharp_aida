namespace LegacySecurityManager;

public record UserInput
{
    private readonly string _username;
    private readonly string _fullName;
    private readonly string _password;
    private readonly string _confirmPassword;

    public UserInput(string username, string fullName, string password, string confirmPassword)
    {
        _username = username;
        _fullName = fullName;
        _password = password;
        _confirmPassword = confirmPassword;
    }

    public bool PasswordsDoNotMatch()
    {
        return _password != _confirmPassword;
    }

    public bool IsPasswordToShort()
    {
        return _password.Length < 8;
    }

    public string EncryptPassword(Cipher cipher)
    {
        return cipher.Encrypt(_password);
    }

    public string UserName()
    {
        return _username;
    }

    public string FullName()
    {
        return _fullName;
    }

    public override string ToString()
    {
        return
            $"{nameof(_username)}: {_username}, " +
            $"{nameof(_fullName)}: " + $"{_fullName}, " +
            $"{nameof(_password)}: {_password}, " +
            $"{nameof(_confirmPassword)}: {_confirmPassword}";
    }
}