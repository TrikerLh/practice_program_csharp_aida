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
    }

    public void NotifySelectDrink()
    {
        _drinkMakerDriver.Notify(CreateMessage());
    }

    private Message CreateMessage()
    {
        var messageContent = _messageCulture.Name switch
        {
            "en-GB" => "Please, select a drink!",
            "es-ES" => "Por favor, ¡selecciona una bebida!",
            "es-PR" => "Por favor, ¡selecciona una bebida!",
            _ => string.Empty
        };

        return Message.Create(messageContent);
    }
}