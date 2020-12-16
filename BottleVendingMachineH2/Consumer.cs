using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    public class Consumer
    {
        private DrinkBuffer drinkBuffer;

        public DrinkBuffer DrinkBuffer
        {
            get { return drinkBuffer; }
            set { drinkBuffer = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Consumer(DrinkBuffer drinkBuffer, string name)
        {
            DrinkBuffer = drinkBuffer;
            Name = name;
        }


        public void GetDrink()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                if (Monitor.TryEnter(DrinkBuffer))
                {
                    while (DrinkBuffer.GetBufferCurrent() == 0)
                    {
                        Console.WriteLine(Name + " is waiting for the machine to refill");
                        Monitor.Wait(DrinkBuffer);
                    }
                    for (int i = 0; i < DrinkBuffer.Drinks.Length; i++)
                    {
                        if (DrinkBuffer.Drinks[i] != null)
                        {
                            Console.WriteLine(Name + " Took a " + DrinkBuffer.Drinks[i].DrinkName + " and drank it");
                            DrinkBuffer.Drinks[i] = null;
                            i = DrinkBuffer.Drinks.Length + 1;
                        }
                    }
                    Monitor.Pulse(DrinkBuffer);
                    Monitor.Exit(DrinkBuffer);
                }
            }

        }

    }
}
