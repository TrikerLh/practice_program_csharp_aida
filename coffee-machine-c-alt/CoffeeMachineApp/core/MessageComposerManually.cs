using System.Collections.Generic;
using System.Globalization;

namespace CoffeeMachineApp.core;

public class MessageComposerManually : MessageComposer
{
    private IDictionary<string, string> _selectDrinkMessages = new Dictionary<string, string>()
    {
        { "es", "¡Por favor, seleccione bebida!"}
        , { "en", "Please, select drink!"}
    };

    private readonly CultureInfo _currentCultureInfo;

    public MessageComposerManually(CultureInfo currentCultureInfo)
    {
        _currentCultureInfo = currentCultureInfo;
    }

    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return Message.Create($"You are missing {missingPrice.ToString(_currentCultureInfo)}");
    }

    public Message ComposeSelectDrinkMessage()
    {
        return Message.Create(_selectDrinkMessages[_currentCultureInfo.TwoLetterISOLanguageName]);
    }
}