namespace CoffeeMachine.core;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private Order _order;
    private decimal _amount;

    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver)
    {
        _drinkMakerDriver = drinkMakerDriver;
        _order = new Order();
        _amount = 0;
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
        _order.AddSpoonOfSugar();
    }

    public void MakeDrink()
    {
        if (NoDrinkWasSelected())
        {
            _drinkMakerDriver.Notify(SelectDrinkMessage());
            return;
        }

        var drinkPrice = GetDrinkPrice();

        var diff = drinkPrice - _amount;

        if (diff > 0)
        {
            _drinkMakerDriver.Notify(Message.Create($"You must add {diff} euros to make this drink"));
            return;
        }

        _drinkMakerDriver.Send(_order);
        _order = new Order();
    }

    private decimal GetDrinkPrice()
    {
        decimal drinkPrice;

        if (_order.GetDrinkType() == DrinkType.Chocolate)
        {
            drinkPrice = (decimal)0.5;
        }
        else if (_order.GetDrinkType() == DrinkType.Tea)
        {
            drinkPrice = (decimal)0.4;
        }
        else
        {
            drinkPrice = (decimal)0.6;
        }

        return drinkPrice;
    }

    private bool NoDrinkWasSelected()
    {
        return _order.GetDrinkType() == DrinkType.None;
    }

    private Message SelectDrinkMessage()
    {
        const string message = "Please, select a drink!";
        return Message.Create(message);
    }

    public void AddMoney(decimal amount)
    {
        _amount = amount;
    }
}