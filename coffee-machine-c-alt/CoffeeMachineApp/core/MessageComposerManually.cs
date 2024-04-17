namespace CoffeeMachineApp.core;

public class MessageComposerManually : MessageComposer
{
    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return default;
    }

    public Message ComposeSelectDrinkMessage()
    {
        return Message.Create("¡Por favor, seleccione bebida!");
    }
}