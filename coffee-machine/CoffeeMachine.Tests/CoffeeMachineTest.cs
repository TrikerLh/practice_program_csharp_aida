using NSubstitute;
using NUnit.Framework;
using static CoffeeMachine.Tests.OrderBuilder;

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

            drinkMakerDriver.Received().Send(Chocolate().Build());
        }

        [Test]
        public void Make_Tea()
        {
            coffeeMachine.SelectTea();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(Tea().Build());
        }

        [Test]
        public void Make_Coffee() {
            coffeeMachine.SelectCoffee();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(Coffee().Build());
        }

        [Test]
        public void Make_Chocolate_whit_one_spoon_of_sugar()
        {
            coffeeMachine.SelectChocolate();
            coffeeMachine.AddOneSpoonOfSugar();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(1).Build());
        }

        [Test]
        public void Make_Chocolate_whit_two_spoon_of_sugar()
        {
            coffeeMachine.SelectChocolate();
            coffeeMachine.AddOneSpoonOfSugar();
            coffeeMachine.AddOneSpoonOfSugar();
            coffeeMachine.MakeDrink();

            drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
        }
    }
}