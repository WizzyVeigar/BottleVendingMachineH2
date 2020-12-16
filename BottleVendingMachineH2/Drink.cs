using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    public class Drink
    {
        private int drinkId;

        public int DrinkId
        {
            get { return drinkId; }
            set { drinkId = value; }
        }

        private string drinkName;

        public string DrinkName
        {
            get { return drinkName; }
            set { drinkName = value; }
        }

        public Drink(int drinkId, string drinkName)
        {
            DrinkId = drinkId;
            DrinkName = drinkName;
        }
    }
}
