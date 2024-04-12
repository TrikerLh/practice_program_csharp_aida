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
        public void Serve_Coffee() {
            driver800.Send(Coffee().Build());

            drinkMaker.Received().Execute("C::");
        }

        [Test]
        public void Serve_Chocolate_whit_one_spoon_of_sugar()
        {
            driver800.Send(Chocolate().WithSpoonsOfSugar(1).Build());

            drinkMaker.Received().Execute("H:1:0");
        }
    }
}
