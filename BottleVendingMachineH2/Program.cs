using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BottleVendingMachineH2
{
    class Program
    {
        static void Main(string[] args)
        {
            DrinkBuffer drinkBuffer = new DrinkBuffer(new Drink[20]);
            DrinkBuffer sodaBuffer = new DrinkBuffer(new Drink[10]);
            DrinkBuffer beerBuffer = new DrinkBuffer(new Drink[10]);
            Producer producer = new Producer(drinkBuffer);
            Producer producer2 = new Producer(drinkBuffer);
            Splitter splitter = new Splitter(drinkBuffer, beerBuffer, sodaBuffer);
            Consumer consumer = new Consumer(beerBuffer, "Lars");
            Consumer consumer2 = new Consumer(sodaBuffer, "Bob");

            Thread producerThread = new Thread(producer.ProduceDrink);
            Thread producerThread2 = new Thread(producer.ProduceDrink);
            Thread splitterThread = new Thread(splitter.SplitDrinks);
            Thread consumerThread = new Thread(consumer.GetDrink);
            Thread consumerThread2 = new Thread(consumer2.GetDrink);

            producerThread.Start();
            splitterThread.Start();
            consumerThread.Start();
            //consumerThread2.Start();
        }
    }
}
