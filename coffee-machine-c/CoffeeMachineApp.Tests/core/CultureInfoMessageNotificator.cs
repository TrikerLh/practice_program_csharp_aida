using System.Globalization;
using CoffeeMachineApp.core;

namespace CoffeeMachineApp.Tests.core;

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
        _drinkMakerDriver.Notify(CreateMissingPriceMessage(missingPrice));
    }

    public void NotifySelectDrink()
    {
        _drinkMakerDriver.Notify(CreateSelectDrinkMessage());
    }

    private Message CreateSelectDrinkMessage()
    {
        var messageContent = _messageCulture.Name switch
        {
            "en-GB" => "Please, select a drink!",
            "es-ES" or "es-PR" => "Por favor, ¡selecciona una bebida!",
            _ => string.Empty
        };

        return Message.Create(messageContent);
    }

    private Message CreateMissingPriceMessage(decimal missingPrice)
    {
        var missingPriceString = missingPrice.ToString(_messageCulture);
        var messageContent = _messageCulture.Name switch
        {
            "en-GB" => "You missing {0}",
            "es-ES" or "es-PR" => "Te faltan {0}",
            _ => string.Empty
        };

        return Message.Create(string.Format(messageContent, missingPriceString));
    }
}