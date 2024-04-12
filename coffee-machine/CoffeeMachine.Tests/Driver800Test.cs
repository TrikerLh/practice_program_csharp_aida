using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class Driver800Test
    {
        private DrinkMaker drinkMaker;
        private Driver800 driver800;

        [SetUp]
        public void SetUp()
        {
            drinkMaker = Substitute.For<DrinkMaker>();
            driver800 = new Driver800(drinkMaker);
        }

        [Test]
        public void Serve_Chocolate()
        {
            Order order = new Order(DrinkType.Chocolate);

            driver800.Send(order);

            drinkMaker.Received().Execute("H::");
        }

        [Test]
        public void Serve_Tea()
        {
            Order order = new Order(DrinkType.Tea);

            driver800.Send(order);

            drinkMaker.Received().Execute("T::");
        }
    }
}
