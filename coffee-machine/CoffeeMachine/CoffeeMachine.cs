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

    public void SelectTea()
    {
        order = new Order(DrinkType.Tea);
    }

    public void SelectCoffee()
    {
        order = new Order(DrinkType.Coffee);
    }

    public void MakeDrink()
    {
        drinkMakerDriver.Send(order);
    }
}