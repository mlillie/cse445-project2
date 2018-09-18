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
        Calendar c = new Calendar();
        private MultiCellBuffer buffer;
        public static event Program.PriceCutEvent priceCut;

        private double base_price = 0; //price of books from pricemodel
        private double discount = 0; //discount to be used for that day
        private int p; // number of price cuts
        private double past_price = 0;
        private double new_price = 0;
        private int publisherId;

        public Publisher(MultiCellBuffer buffer, int publisherId)
        {
            this.buffer = buffer;
            this.publisherId = publisherId;
            base_price = (c.Base_Price(c.Day())).Item1;
            past_price = base_price;
            discount = (c.Base_Price(c.Day()).Item2);
        }


        //Moved this to a function for when starting the threads in Program
        public void PublisherFunction()
        {

            while (p < 5)
            {
                Thread.Sleep(2000); // Should make this randomly probably

                Monitor.Enter(buffer);
                try
                {
                    string s = buffer.ReadFromBuffer();
                    if (s != null)
                    {
                        // determines price cut using model
                        new_price = PricingModel(s);
                        if (new_price < past_price)
                        {
                            if(priceCut != null) //Added null check
                                priceCut(publisherId, new_price);
                            p++;
                            Console.WriteLine("Publisher # " + publisherId + "; P IS : " + p.ToString());
                            past_price = new_price;
                        }
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
                finally
                {
                    Monitor.Exit(buffer);
                }
            }
            Console.Write(Thread.CurrentThread.Name.ToString() + " is terminating.\n");
            Program.setRunning(publisherId, false);
        }

        public double PricingModel(string s)
        {
            OrderClass order = Project2.Cipher.decoder(s);

            double final_price = base_price + (base_price * discount);

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