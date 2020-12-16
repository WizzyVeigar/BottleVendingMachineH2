using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    class Producer
    {
        private DrinkBuffer buffer;

        public DrinkBuffer Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        public Producer(DrinkBuffer buffer)
        {
            Buffer = buffer;
        }

        bool drinkName = false;
        Random random = new Random();
        int drinkId = 0;

        public void ProduceDrink()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                if (Monitor.TryEnter(Buffer))
                {
                    while (Buffer.GetBufferCurrent() >= Buffer.Drinks.Length)
                    {
                        //Console.WriteLine("Producer waiting");
                        Monitor.Wait(Buffer);
                    }

                    for (int i = 0; i < Buffer.Drinks.Length; i++)
                    {
                        if (Buffer.Drinks[i] == null)
                        {
                            Console.WriteLine("Producer Added to the buffer");
                            if (drinkName)
                            {
                                Buffer.Drinks[i] = new Drink(drinkId, "Sodavand");
                                drinkName = false;
                            }
                            else
                            {
                                Buffer.Drinks[i] = new Drink(drinkId, "Beer");
                                drinkName = true;
                            }
                            drinkId++;
                            i = Buffer.Drinks.Length + 1;
                        }
                    }
                    Monitor.Pulse(Buffer);
                    Monitor.Exit(Buffer);
                }
            }
        }
    }
}
