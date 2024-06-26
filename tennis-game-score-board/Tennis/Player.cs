namespace Tennis;

public class Player
{
    private int _point;

    public void AddPoint()
    {
        _point++;
    }

    public string GetMessagePoint()
    {
        return _point switch
        {
            0 => "Love",
            1 => "Fifteen",
            2 => "Thirty",
            3 => "Forty",
            _ => ""
        };
    }

    public int Points()
    {
        return _point;
    }
}