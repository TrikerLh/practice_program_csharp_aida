namespace InteractiveCheckout;

public class UserConfirmation
{
    private readonly bool _accepted;

    public UserConfirmation()
    {
        
    }
    public UserConfirmation(string message)
    {
        Console.WriteLine($"{message} Choose Option (Y yes) (N no):");
        var result = Console.ReadLine();
        _accepted = result != null && result.ToLower() == "y";
    }

    public virtual bool WasAccepted()
    {
        return _accepted;
    }
}