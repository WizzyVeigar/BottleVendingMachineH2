using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    public class DrinkBuffer
    {
        private Drink[] drinks;
        public Drink[] Drinks
        {
            get { return drinks; }
            set { drinks = value; }
        }

        public int GetBufferCurrent()
        {
            int amount = 0;
            for (int i = 0; i < Drinks.Length; i++)
            {
                if (Drinks[i] != null)
                {
                    amount++;
                }
            }
            return amount;
        }
        public DrinkBuffer(Drink[] drinks)
        {
            Drinks = drinks;
        }

    }
}
