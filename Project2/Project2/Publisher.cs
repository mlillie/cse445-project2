using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    // Created by Jacqueline Fonseca on 09/08/2018
    class Publisher
    {
        private double price = 0; //price of books from pricemodel
        private int p; // number of price cuts
 
        public Publisher()
        {
            double newPrice = PricingModel;
            if (newPrice < price)
            {
                //TODO: event handler to BOOKSTORE

            }

        }

        public double PricingModel
        {
            // TODO: make a mathematical model to determine price
            // we're using a random number generator for now
            get
            {
                Random random = new Random();
                int num = random.Next(50, 201);
                return num;
            }
        }
    }
}
