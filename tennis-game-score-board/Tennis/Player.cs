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

    public bool HasWon()
    {
        return _point == 4;
    }

    public bool IsDeuce(Player playerTwo)
    {
        return _point == 3 && playerTwo._point == 3;
    }

    public bool HasAdvantage(Player playerTwo)
    {
        if (this._point > playerTwo._point) return true;
        return false;
    }
}