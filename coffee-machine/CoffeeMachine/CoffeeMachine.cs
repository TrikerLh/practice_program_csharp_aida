namespace CoffeeMachine;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver drinkMakerDriver;
    private Order order;


    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver)
    {
        this.drinkMakerDriver = drinkMakerDriver;
    }

    public void SelectChocolate()
    {
        order = new Order(DrinkType.Chocolate);
    }

    public void MakeDrink()
    {
        drinkMakerDriver.Send(order);
    }
}