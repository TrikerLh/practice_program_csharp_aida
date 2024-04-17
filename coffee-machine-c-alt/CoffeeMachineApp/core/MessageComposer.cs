namespace CoffeeMachineApp.core;

public class MessageComposer
{
    public Message ComposeMissingMoneyMessage(decimal missingPrice)
    {
        return Message.Create($"You are missing {missingPrice}");
    }
}