namespace CoffeeMachine
{
    public class Driver800 : DrinkMakerDriver
    {
        private DrinkMaker drinkMaker;
        private string command;

        public Driver800(DrinkMaker drinkMaker)
        {
            this.drinkMaker = drinkMaker;
        }

        public void Send(Order order)
        {
            command = ToCommand(order);
            drinkMaker.Execute(command);
        }
        private string ToCommand(Order order)
        {
            return $"{(char)order.GetDrinkType()}::";
        }
    }
}
