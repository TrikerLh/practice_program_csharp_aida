using CoffeeMachine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using static CoffeeMachine.Tests.helpers.OrderBuilder;

namespace CoffeeMachine.Tests.core {
    public class CoffeeMachineWithMoneyTest {
        private CoffeeMachine.core.CoffeeMachine _coffeeMachine;
        private DrinkMakerDriver _drinkMakerDriver;

        [SetUp]
        public void SetUp() {
            _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
            _coffeeMachine = new CoffeeMachine.core.CoffeeMachine(_drinkMakerDriver, new Dictionary<DrinkType, decimal>
            {
                { DrinkType.Tea, 0.4m },
                { DrinkType.Chocolate , 0.5m },
                { DrinkType.Coffee , 0.6m }
            });
        }

        [Test]
        public void Make_Chocolate_without_enough_money() {
            _coffeeMachine.SelectChocolate();
            _coffeeMachine.AddMoney((decimal)0.2);
            _coffeeMachine.MakeDrink();

            _drinkMakerDriver.Received().Notify(Message.Create("You must add 0,3 euros to make this drink"));
            _drinkMakerDriver.Received(0).Send(Arg.Any<Order>());
        }

        [Test]
        public void Make_Tea_without_enough_money()
        {
            _coffeeMachine.SelectTea();
            _coffeeMachine.AddMoney((decimal)0.2);
            _coffeeMachine.MakeDrink();

            _drinkMakerDriver.Received().Notify(Message.Create("You must add 0,2 euros to make this drink"));
            _drinkMakerDriver.Received(0).Send(Arg.Any<Order>());
        }

        [Test]
        public void Make_Coffee_without_enough_money() {
            _coffeeMachine.SelectCoffee();
            _coffeeMachine.AddMoney((decimal)0.2);
            _coffeeMachine.MakeDrink();

            _drinkMakerDriver.Received().Notify(Message.Create("You must add 0,4 euros to make this drink"));
            _drinkMakerDriver.Received(0).Send(Arg.Any<Order>());
        }

    }
}
