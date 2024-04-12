using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Tests {
    public class OrderBuilder {
        private int spoonsOfSugar;
        private DrinkType drink;

        private OrderBuilder()
        {
            spoonsOfSugar = 0;
            drink = DrinkType.None;
        }

        public static OrderBuilder Chocolate()
        {
            var builder = new OrderBuilder();
            builder.drink = DrinkType.Chocolate;
            return builder;
        }

        public static OrderBuilder Tea() {
            var builder = new OrderBuilder();
            builder.drink = DrinkType.Tea;
            return builder;
        }

        public static OrderBuilder Coffee() {
            var builder = new OrderBuilder();
            builder.drink = DrinkType.Coffee;
            return builder;
        }

        public OrderBuilder WithSpoonsOfSugar(int spoonsOfSugar)
        {
            this.spoonsOfSugar = spoonsOfSugar;
            return this;
        }

        public Order Build()
        {
            return new Order(drink, spoonsOfSugar);
        }
    }
}
