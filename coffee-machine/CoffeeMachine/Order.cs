using System;

namespace CoffeeMachine;

public class Order
{
    private readonly DrinkType _drinkType;
    private int _spoonOfSugar;


    public Order(DrinkType drinkType) {
        _drinkType = drinkType;
        _spoonOfSugar = 0;
    }

    public DrinkType GetDrinkType()
    {
        return _drinkType;
    }

    public void AddSpoonOfSugar()
    {
        _spoonOfSugar += 1;
    }

    protected bool Equals(Order other)
    {
        return _drinkType == other._drinkType && _spoonOfSugar == other._spoonOfSugar;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Order)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)_drinkType, _spoonOfSugar);
    }

    public override string ToString() {
        return $"{nameof(_drinkType)}: {_drinkType}, {nameof(_spoonOfSugar)}: {_spoonOfSugar}";
    }

}