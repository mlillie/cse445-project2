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
        Calendar c = new Calendar();

        private double base_price = 0; //price of books from pricemodel
        private double discount = 0; //discount to be used for that day
        private int p; // number of price cuts
        private double past_price = 0; 
        private double new_price = 0;
        
        public Publisher()
        {
            base_price = (c.Base_Price(c.Day())).Item1;
            discount = (c.Base_Price(c.Day()).Item2);

            new_price = PricingModel(" ");
            if (new_price < past_price)
            {
                //event
                past_price = new_price;
            }
            else
                past_price = new_price;
        }

        public double PricingModel(string s)
        {
            Cipher cipher = new Cipher();
            OrderClass order = cipher.decoder(s);

            double final_price = base_price * discount;
            if (order.Amount % 2 == 0 && order.Amount > 10)
                final_price = final_price + (order.Amount * discount);
            else
                final_price = final_price - (order.Amount * discount);
            return final_price; 
        }
    }
}
