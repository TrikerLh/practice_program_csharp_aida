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

    private Message CreateSelectDrinkMessage() => GenerateMessage(GetSelectDrinkMessageContent());

    private string GetSelectDrinkMessageContent() =>
        _messageCulture.TwoLetterISOLanguageName switch
        {
            "en" => "Please, select a drink!",
            "es" => "Por favor, ¡selecciona una bebida!",
            _ => string.Empty
        };

    private Message CreateMissingPriceMessage(decimal missingPrice) => GenerateMessage(string.Format(GetMissingPriceMessageContent(), GetMissingPriceFormatted(missingPrice)));

    private string GetMissingPriceMessageContent() =>
        _messageCulture.TwoLetterISOLanguageName switch
        {
            "en" => "You missing {0}",
            "es" => "Te faltan {0}",
            _ => string.Empty
        };

    private string GetMissingPriceFormatted(decimal missingPrice) =>
        missingPrice.ToString(_messageCulture);

    private static Message GenerateMessage(string messageContent) => Message.Create(messageContent);
}