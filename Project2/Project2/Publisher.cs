using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Project2
{
    // Created by Jacqueline Fonseca on 09/08/2018

    class Publisher
    {
        // Calendar that will get the daily base price of books and the daily discount
        Calendar c = new Calendar();

        // Buffer that is passed in the constructor; used for getting orders
        private MultiCellBuffer buffer;

        // Event that will notify BookStore class 
        public static event Program.PriceCutEvent priceCut;

        // Price of books from Calendar
        private double base_price;

        // Discount to be used for that day
        private double discount;

        // Will keep track of total number of price cuts
        private int p;

        // Will keep track of the previous price (before PriceModel is called)
        private static double past_price;

        // Keeps track of value returned by PriceModel
        private static double new_price;

        // ID of the thread
        private int publisherId;

        // Random number generator
        private Random random;

        // Constructor that gets all the stuff
        public Publisher(MultiCellBuffer buffer, int publisherId)
        {
            this.buffer = buffer;
            this.publisherId = publisherId;
            base_price = (c.Base_Price(c.Day())).Item1;
            past_price = base_price;
            discount = (c.Base_Price(c.Day()).Item2);
            this.random = new Random();
        }


        // Function for starting the threads in Program
        public void PublisherFunction()
        {

            while (p < 20)
            {
                Thread.Sleep(random.Next(1500, 4000));

                string s = buffer.ReadFromBuffer(); // Gets order string
                if (s != null)
                {
                    // Determines new price using the PriceModel
                    new_price = PricingModel(s);

                    // Lower new price means p is increased and priceCut event is exectuted
                    // Print new price
                    if (new_price < past_price)
                    {
                        p++;
                        Console.Write(">> Price decreased for Publisher # " + publisherId + " from $" + past_price.ToString("0.00") +
                            " to $" + new_price.ToString("0.00") + "\n");
                        past_price = new_price;
                        if (priceCut != null) //Added null check
                            priceCut(publisherId, new_price);
                    }

                    // Print new increased price
                    else if (new_price > past_price)
                    {
                        Console.Write(">> Price increased for Publisher # " + publisherId + " from $" + past_price.ToString("0.00") +
                            " to $" + new_price.ToString("0.00") + "\n");
                        past_price = new_price;
                    }
                    // If price stays the same, print nothing
                    else
                        past_price = new_price;

                    // starts OrderProcessing thread
                    OrderClass order = Cipher.decoder(s);
                    order.Unit_Price = new_price;
                    //I changed the reciever id to just the name of the thread
                    order.ReceiverId = Thread.CurrentThread.Name.ToString();
                    Thread t = new Thread(() => Project2.OrderProcessingThread.OPT(order));
                    t.Start();
                }
            }
            Console.Write(Thread.CurrentThread.Name.ToString() + " is terminating.\n");
            Program.setRunning(publisherId, false);
        }

        public double PricingModel(string s)
        {
            OrderClass order = Project2.Cipher.decoder(s);

            // Gets current price
            double final_price = new_price;

            // Will change the price to discounted one
            if (order.Amount % 2 == 0 && order.Amount > 10)
                final_price = final_price + (order.Amount * discount);
            else
                final_price = final_price - (order.Amount * discount);

            // Checks whether the price is too low or high
            while (final_price < 50)
            {
                final_price += 5;
            }
            while (final_price > 200)
            {
                final_price -= 3;
            }
            return final_price;

        }
    }
}