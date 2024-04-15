namespace CoffeeMachine;

public class CoffeeMachine
{
    private readonly DrinkMaker _drinkMaker;
    private readonly Order _order;

    public CoffeeMachine(DrinkMaker drinkMaker)
    {
        _drinkMaker = drinkMaker;
        _order = new Order();
    }

    public void SelectChocolate()
    {
        _order.SelectDrink(DrinkType.Chocolate);
    }

    public void SelectTea()
    {
        _order.SelectDrink(DrinkType.Tea);
    }

    public void SelectCoffee()
    {
        _order.SelectDrink(DrinkType.Coffee);
    }

    public void AddOneSpoonOfSugar()
    {
        _order.AddOneSpoonOfSugar();
    }

    public void MakeDrink()
    {
        _drinkMaker.Execute(_order.ToCommand());
    }
}