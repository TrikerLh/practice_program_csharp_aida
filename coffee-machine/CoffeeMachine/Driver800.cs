namespace CoffeeMachine
{
    public class Driver800 : DrinkMakerDriver
    {
        private DrinkMaker drinkMaker;

        public Driver800(DrinkMaker drinkMaker)
        {
            this.drinkMaker = drinkMaker;
        }

        public void Send(Order order)
        {
            drinkMaker.Execute(ToCommand(order));
        }
        private string ToCommand(Order order)
        {
            return $"{(char)order.GetDrinkType()}::";
        }
    }
}
