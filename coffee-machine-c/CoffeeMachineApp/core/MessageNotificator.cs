namespace CoffeeMachineApp.core;

public interface MessageNotificator
{
    void NotifyMissingPrice(decimal missingPrice);
    void NotifySelectDrink();
}