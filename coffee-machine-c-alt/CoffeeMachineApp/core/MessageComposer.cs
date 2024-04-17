namespace CoffeeMachineApp.core;

public interface MessageComposer
{
    Message ComposeMissingMoneyMessage(decimal missingPrice);
    Message ComposeSelectDrinkMessage();
}