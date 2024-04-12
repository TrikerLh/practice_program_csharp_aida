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
            drinkMaker.Execute(ComposeCommand(order));
        }
        private string ComposeCommand(Order order)
        {
            return ComposeDrinkSection(order) + ":" + ComposeSugarSection(order);
        }

        private static string ComposeDrinkSection(Order order)
        {
            return $"{(char)order.GetDrinkType()}";
        }

        private string ComposeSugarSection(Order order)
        {
            var spoonsOfSugar = order.GetSpoonsOfSugar();
            var spoonOfSugarToString = spoonsOfSugar > 0 ? spoonsOfSugar.ToString() : string.Empty;
            var hasStickToString = spoonsOfSugar > 0 ? "0" : string.Empty;
            var sugarSection = $"{spoonOfSugarToString}:{hasStickToString}";
            return sugarSection;
        }
    }
}
