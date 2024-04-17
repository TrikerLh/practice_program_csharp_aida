namespace CoffeeMachineApp.core;

public class MessageComposerManually
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