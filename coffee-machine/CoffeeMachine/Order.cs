namespace CoffeeMachine;

public class Order
{
    private readonly DrinkType _drinkType;

    public Order(DrinkType drinkType)
    {
        _drinkType = drinkType;
    }

    protected bool Equals(Order other)
    {
        return _drinkType == other._drinkType;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Order) obj);
    }

    public override int GetHashCode()
    {
        return (int) _drinkType;
    }

    
}