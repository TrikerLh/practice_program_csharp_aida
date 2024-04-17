using System.Collections.Generic;
using System.Globalization;

namespace CoffeeMachineApp.core;

public class MessageComposerManually : MessageComposer
{
    private IDictionary<CultureInfo, string> _selectDrinkMessages = new Dictionary<CultureInfo, string>()
    {
        { new CultureInfo("es-ES"), "¡Por favor, seleccione bebida!"}
        , { new CultureInfo("en-GB"), "Please, select drink!"}
    };

    private readonly CultureInfo _currentCultureInfo;

    public MessageComposerManually(CultureInfo currentCultureInfo)
    {
        _currentCultureInfo = currentCultureInfo;
    }

    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return default;
    }

    public Message ComposeSelectDrinkMessage()
    {
        return Message.Create(_selectDrinkMessages[_currentCultureInfo]);
    }
}