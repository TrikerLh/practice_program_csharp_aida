using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class CoffeeMachineTest
    {
        [Test]
        public void Make_Chocolate()
        {
            DrinkMakerDriver drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
            CoffeeMachine coffeeMachine = new CoffeeMachine(drinkMakerDriver);

            coffeeMachine.SelectChocolate();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(new Order(DrinkType.Chocolate));
        }

        [Test]
        public void Make_Tea()
        {
            DrinkMakerDriver drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
            CoffeeMachine coffeeMachine = new CoffeeMachine(drinkMakerDriver);

            coffeeMachine.SelectTea();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(new Order(DrinkType.Tea));
        }
    }
}