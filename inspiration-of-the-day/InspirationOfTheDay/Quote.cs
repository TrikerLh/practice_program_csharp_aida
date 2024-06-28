namespace InspirationOfTheDay;

public class Quote
{
    private readonly string _text;

    public Quote(string text)
    {
        _text = text;
    }

    public string Text()
    {
        return _text; 
    }
}