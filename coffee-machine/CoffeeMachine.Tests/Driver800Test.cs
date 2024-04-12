using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class Driver800Test
    {
        [Test]
        public void Serve_Chocolate()
        {
            DrinkMaker drinkMaker = Substitute.For<DrinkMaker>();
            Order order = new Order(DrinkType.Chocolate);
            Driver800 driver800 = new Driver800(drinkMaker);

            driver800.Send(order);

            drinkMaker.Received().Execute("C::");
        }

        [Test]
        public void Serve_Tea()
        {
            DrinkMaker drinkMaker = Substitute.For<DrinkMaker>();
            Order order = new Order(DrinkType.Tea);
            Driver800 driver800 = new Driver800(drinkMaker);

            driver800.Send(order);

            drinkMaker.Received().Execute("T::");
        }
    }
}
