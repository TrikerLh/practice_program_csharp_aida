using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
