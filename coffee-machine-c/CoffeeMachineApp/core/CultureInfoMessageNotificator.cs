using System.Globalization;

namespace CoffeeMachineApp.core;

public class CultureInfoMessageNotificator : MessageNotificator
{
    private readonly CultureInfo _messageCulture;
    private readonly DrinkMakerDriver _drinkMakerDriver;

    public CultureInfoMessageNotificator(CultureInfo messageCulture, DrinkMakerDriver drinkMakerDriver)
    {
        _messageCulture = messageCulture;
        _drinkMakerDriver = drinkMakerDriver;
    }

    public void NotifyMissingPrice(decimal missingPrice)
    {
    }

    public void NotifySelectDrink()
    {
        _drinkMakerDriver.Notify(Message.Create("Please, select a drink!"));
    }
}