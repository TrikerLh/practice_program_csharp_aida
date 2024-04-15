namespace CoffeeMachine;

public class Order
{
    private const int MaxSugarSpoons = 2;
    private DrinkType _drinkType;
    private int _spoonsOfSugar;

    public Order()
    {
        _drinkType = DrinkType.None;
        _spoonsOfSugar = 0;
    }

    public void SelectDrink(DrinkType drinkType)
    {
        _drinkType = drinkType;
    }

    public virtual string ToCommand()
    {
        var hasStickToString = _spoonsOfSugar > 0 ? "0" : string.Empty;
        var sugarSpoonToString = _spoonsOfSugar == 0 ? string.Empty : _spoonsOfSugar.ToString();
        return $"{(char)_drinkType}:{sugarSpoonToString}:{hasStickToString}";
    }

    public void AddOneSpoonOfSugar()
    {
        if (_spoonsOfSugar < MaxSugarSpoons) _spoonsOfSugar += 1;
    }
}