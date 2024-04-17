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

    private IDictionary<string, string> _missingPriceMessages = new Dictionary<string, string>()
    {
        { "es", "Moroso, paga lo que falta: {0} Primer aviso"}
        , { "en", "You are missing {0}"}
    };

    private readonly CultureInfo _currentCultureInfo;

    public MessageComposerManually(CultureInfo currentCultureInfo)
    {
        _currentCultureInfo = currentCultureInfo;
    }

    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return Message.Create(string.Format(_missingPriceMessages[_currentCultureInfo.TwoLetterISOLanguageName], missingPrice.ToString(_currentCultureInfo)));
    }

    public Message ComposeSelectDrinkMessage()
    {
        return Message.Create(_selectDrinkMessages[_currentCultureInfo.TwoLetterISOLanguageName]);
    }
}