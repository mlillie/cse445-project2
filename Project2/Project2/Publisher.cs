using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Project2
{

    // Created by Jacqueline Fonseca on 09/08/2018
    public delegate void PriceCutEvent();

    class Publisher
    {
        Calendar c = new Calendar();
        private MultiCellBuffer buffer;
        public static event PriceCutEvent priceCut;

        private double base_price = 0; //price of books from pricemodel
        private double discount = 0; //discount to be used for that day
        private int p; // number of price cuts
        private double past_price = 0; 
        private double new_price = 0;
        
        public Publisher(MultiCellBuffer buffer)
        {
            this.buffer = buffer;

            base_price = (c.Base_Price(c.Day())).Item1;
            past_price = base_price;
            discount = (c.Base_Price(c.Day()).Item2);

            while (p < 20)
            {
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
                            priceCut();
                            p++;
                            past_price = new_price;
                        }
                        else
                            past_price = new_price;
                        // starts OrderProcessing thread
                        OrderClass order = Cipher.decoder(s);
                        order.Unit_Price = new_price;
                        order.ReceiverId = Thread.CurrentThread.ManagedThreadId.ToString();
                        Thread t = new Thread(() => Project2.OrderProcessingThread.OPT(order));
                        t.Start(order);
                    }
                }
                finally
                {
                    Monitor.Exit(buffer);
                }
            }
        }

        public double PricingModel(string s)
        {
            OrderClass order = Project2.Cipher.decoder(s);

            double final_price = base_price * discount;
            // Will change the price
            if (order.Amount % 2 == 0 && order.Amount > 10)
                final_price = final_price + (order.Amount * discount);
            else
                final_price = final_price - (order.Amount * discount);
            return final_price; 
        }
    }
}
