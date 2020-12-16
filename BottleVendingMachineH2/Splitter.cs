using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    class Splitter
    {
        private DrinkBuffer incomingBuffer;

        public DrinkBuffer IncomingBuffer
        {
            get { return incomingBuffer; }
            set { incomingBuffer = value; }
        }

        private DrinkBuffer beerBuffer;

        public DrinkBuffer BeerBuffer
        {
            get { return beerBuffer; }
            set { beerBuffer = value; }
        }

        private DrinkBuffer sodaBuffer;

        public DrinkBuffer SodaBuffer
        {
            get { return sodaBuffer; }
            set { sodaBuffer = value; }
        }

        public Splitter(DrinkBuffer incomingBuffer, DrinkBuffer beerBuffer, DrinkBuffer sodaBuffer)
        {
            IncomingBuffer = incomingBuffer;
            BeerBuffer = beerBuffer;
            SodaBuffer = sodaBuffer;
        }

        /// <summary>
        /// Splits the drinks from the incoming buffer, out into the 2 other buffers
        /// </summary>
        public void SplitDrinks()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                if (Monitor.TryEnter(IncomingBuffer))
                {
                    while (incomingBuffer.GetBufferCurrent() == 0)
                    {
                        Console.WriteLine("IncomingBuffer is empty...");
                        Monitor.Wait(IncomingBuffer);
                    }

                    if (Monitor.TryEnter(SodaBuffer))
                    {
                        if (SodaBuffer.GetBufferCurrent() >= SodaBuffer.Drinks.Length)
                        {
                            Console.WriteLine("SodaBuffer is full");
                            Monitor.Wait(SodaBuffer, 200);
                        }

                        for (int i = 0; i < SodaBuffer.Drinks.Length; i++)
                        {
                            if (SodaBuffer.Drinks[i] == null)
                            {
                                for (int j = 0; j < IncomingBuffer.Drinks.Length; j++)
                                {
                                    if (IncomingBuffer.Drinks[j] != null)
                                    {

                                        if (IncomingBuffer.Drinks[j].DrinkName == "Sodavand")
                                        {
                                            SodaBuffer.Drinks[i] = IncomingBuffer.Drinks[j];
                                            IncomingBuffer.Drinks[j] = null;
                                            j = IncomingBuffer.Drinks.Length + 1;
                                            i = SodaBuffer.Drinks.Length + 1;
                                            Console.WriteLine("Splitter splitted bottle to SodaBuffer");

                                        }
                                    }
                                }
                            }
                        }
                        Monitor.Pulse(SodaBuffer);
                        Monitor.Exit(SodaBuffer);
                    }

                    if (Monitor.TryEnter(BeerBuffer))
                    {
                        if (BeerBuffer.GetBufferCurrent() >= BeerBuffer.Drinks.Length)
                        {
                            Console.WriteLine("BeerBuffer is full");
                            Monitor.Wait(BeerBuffer, 200);
                        }
                        for (int i = 0; i < BeerBuffer.Drinks.Length; i++)
                        {
                            if (BeerBuffer.Drinks[i] == null)
                            {
                                for (int j = 0; j < IncomingBuffer.Drinks.Length; j++)
                                {
                                    if (IncomingBuffer.Drinks[j] != null)
                                    {
                                        if (IncomingBuffer.Drinks[j].DrinkName == "Beer")
                                        {
                                            BeerBuffer.Drinks[i] = IncomingBuffer.Drinks[j];
                                            IncomingBuffer.Drinks[j] = null;
                                            j = IncomingBuffer.Drinks.Length + 1;
                                            i = BeerBuffer.Drinks.Length + 1;
                                            Console.WriteLine("Splitter splitted bottle to BeerBuffer");
                                        }
                                    }
                                }
                            }
                        }
                        Monitor.Pulse(BeerBuffer);
                        Monitor.Exit(BeerBuffer);
                    }
                    Monitor.Pulse(IncomingBuffer);
                    Monitor.Exit(IncomingBuffer);
                }
            }
        }

    }
}
