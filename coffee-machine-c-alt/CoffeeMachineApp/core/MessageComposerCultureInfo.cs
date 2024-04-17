using System.Collections.Generic;
using System.Globalization;

namespace CoffeeMachineApp.core;

public class MessageComposerCultureInfo : MessageComposer
{
    private readonly IDictionary<string, string> _selectDrinkMessages = new Dictionary<string, string>()
    {
        { "es", "¡Por favor, seleccione bebida!"}
        , { "en", "Please, select drink!"}
    };

    private readonly IDictionary<string, string> _missingPriceMessages = new Dictionary<string, string>()
    {
        { "es", "Moroso, paga lo que falta: {0} Primer aviso"}
        , { "en", "You are missing {0}"}
    };

    private readonly CultureInfo _currentCultureInfo;
    private string LanguageCode => _currentCultureInfo.TwoLetterISOLanguageName;

    public MessageComposerCultureInfo(CultureInfo currentCultureInfo)
    {
        _currentCultureInfo = currentCultureInfo;
    }

    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return Message.Create(string.Format(_missingPriceMessages[LanguageCode], missingPrice.ToString(_currentCultureInfo)));
    }

    public Message ComposeSelectDrinkMessage()
    {
        return Message.Create(_selectDrinkMessages[LanguageCode]);
    }
}