namespace CoffeeMachineApp.core;

public class MessageComposerManually : MessageComposer
{
    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return Message.Create($"You are missing {missingPrice}");
    }

    public Message ComposeSelectDrinkMessage()
    {
        const string message = "Please, select a drink!";
        return Message.Create(message);
    }
}