namespace Tennis;

public class Player
{
    private int _point;

    public void AddPoint()
    {
        _point++;
    }

    public int Points()
    {
        return _point;
    }

    public bool IsTiedWith(Player other)
    {
        return _point == other._point;
    }

    public bool HasMorePointsThan(Player other)
    {
        return _point > other._point;
    }

    public bool HasMoreThanFortyPoints()
    {
        return _point > 3;
    }

    public bool HasFortyPoints()
    {
        return _point == 3;
    }
}