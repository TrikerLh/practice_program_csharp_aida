using System.Collections.Generic;

namespace CoffeeMachine.core;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private Order _order;
    private decimal _amount;
    private Dictionary<DrinkType, decimal> _priceList;

    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver, Dictionary<DrinkType, decimal> priceList)
    {
        _drinkMakerDriver = drinkMakerDriver;
        _order = new Order();
        _amount = 0;
        _priceList = priceList;
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
        return _priceList[_order.GetDrinkType()];
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