using NSubstitute;
using NUnit.Framework;
using static CoffeeMachine.Tests.OrderBuilder;

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
            driver800.Send(Chocolate().Build());

            drinkMaker.Received().Execute("H::");
        }

        [Test]
        public void Serve_Tea()
        {
            driver800.Send(Tea().Build());

            drinkMaker.Received().Execute("T::");
        }

        [Test]
        public void Serve_Coffee()
        {
            driver800.Send(Coffee().Build());

            drinkMaker.Received().Execute("C::");
        }

        [TestCase(1, "H:1:0")]
        [TestCase(2, "H:2:0")]
        public void Serve_drink_with_different_spoons_of_sugar(int spoonsOfSugar, string expectedCommand)
        {
            driver800.Send(Chocolate().WithSpoonsOfSugar(spoonsOfSugar).Build());

            drinkMaker.Received().Execute(expectedCommand);
        }

    }
}
