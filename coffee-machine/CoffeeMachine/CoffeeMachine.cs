namespace CoffeeMachine;

public class CoffeeMachine
{
    private readonly DrinkMakerDriver drinkMakerDriver;
    private Order order;


    public CoffeeMachine(DrinkMakerDriver drinkMakerDriver)
    {
        this.drinkMakerDriver = drinkMakerDriver;
        this.order = new Order();
    }

    public void SelectChocolate()
    {
        order.SelectDrink(DrinkType.Chocolate);
    }

    public void SelectTea()
    {
        order.SelectDrink(DrinkType.Tea);
    }

    public void SelectCoffee()
    {
        order.SelectDrink(DrinkType.Coffee);
    }

    public void MakeDrink()
    {
        drinkMakerDriver.Send(order);
    }

    public void AddOneSpoonOfSugar()
    {
        order.AddSpoonOfSugar();
    }
}