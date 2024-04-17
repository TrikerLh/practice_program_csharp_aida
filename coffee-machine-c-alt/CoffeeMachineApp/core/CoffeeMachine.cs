using System.Collections.Generic;

namespace CoffeeMachineApp.core;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private readonly Dictionary<DrinkType, decimal> _prices;
    private readonly MessageComposer _messageComposer;
    private Order _order;
    private decimal _totalMoney;

    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver, Dictionary<DrinkType, decimal> prices, MessageComposer messageComposer)
    {
        _messageComposer = messageComposer;
        _drinkMakerDriver = drinkMakerDriver;
        _prices = prices;
        InitializeState();
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

    public void AddMoney(decimal money)
    {
        _totalMoney += money;
    }

    public void MakeDrink()
    {
        if (NoDrinkWasSelected())
        {
            _drinkMakerDriver.Notify(_messageComposer.ComposeSelectDrinkMessage());
            return;
        }

        if (IsThereEnoughMoney())
        {
            _drinkMakerDriver.Send(_order);
            InitializeState();
        }
        else
        {
            _drinkMakerDriver.Notify(_messageComposer.ComposeMissingMoneyMessage(_prices[_order.GetDrinkType()]-_totalMoney));
        }
    }

    private void InitializeState()
    {
        _totalMoney = 0;
        _order = new Order();
    }

    private bool IsThereEnoughMoney()
    {
        return _totalMoney >= _prices[_order.GetDrinkType()];
    }

    private bool NoDrinkWasSelected()
    {
        return _order.GetDrinkType() == DrinkType.None;
    }
}