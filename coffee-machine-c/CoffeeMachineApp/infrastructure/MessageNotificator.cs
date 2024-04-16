namespace CoffeeMachineApp.infrastructure;

public interface MessageNotificator
{
    void NotifyMissingPrice(decimal missingPrice);
    void NotifySelectDrink();
}