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
        //private const string SelectDrinkMessage = "Please, select a drink!";
        private CoffeeMachine.core.CoffeeMachine _coffeeMachine;
        private DrinkMakerDriver _drinkMakerDriver;

        [SetUp]
        public void SetUp() {
            _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
            _coffeeMachine = new CoffeeMachine.core.CoffeeMachine(_drinkMakerDriver);
        }

        [Test]
        public void Make_Chocolate_without_enough_money() {
            _coffeeMachine.SelectChocolate();
            _coffeeMachine.MakeDrink();

            _drinkMakerDriver.Received().Notify(Message.Create("You must add 0,5 euros to make this drink"));
        }
    }
}
