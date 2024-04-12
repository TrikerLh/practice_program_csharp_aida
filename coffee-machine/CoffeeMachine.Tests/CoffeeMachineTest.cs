using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class CoffeeMachineTest
    {
        private DrinkMakerDriver drinkMakerDriver;
        private CoffeeMachine coffeeMachine;

        [SetUp]
        public void SetUp()
        {
            drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
            coffeeMachine = new CoffeeMachine(drinkMakerDriver);
        }
        [Test]
        public void Make_Chocolate()
        {
            coffeeMachine.SelectChocolate();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(new Order(DrinkType.Chocolate));
        }

        [Test]
        public void Make_Tea()
        {
            coffeeMachine.SelectTea();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(new Order(DrinkType.Tea));
        }

        [Test]
        public void Make_Coffee() {
            coffeeMachine.SelectCoffee();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(new Order(DrinkType.Coffee));
        }

        [Test]
        public void Make_Chocolate_whit_one_spoon_of_sugar()
        {
            var expectedOrder = new Order(DrinkType.Chocolate);
            expectedOrder.AddSpoonOfSugar();

            coffeeMachine.SelectChocolate();
            coffeeMachine.AddOneSpoonOfSugar();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(expectedOrder);
        }
    }
}