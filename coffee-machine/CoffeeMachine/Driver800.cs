using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class Driver800 : DrinkMakerDriver
    {
        private DrinkMaker drinkMaker;

        public Driver800(DrinkMaker drinkMaker)
        {
            this.drinkMaker = drinkMaker;
        }

        public void Send(Order order)
        {
            drinkMaker.Execute("C::");
        }
    }
}
