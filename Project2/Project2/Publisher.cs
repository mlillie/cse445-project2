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
        private double discount = 0;
        private int p; // number of price cuts
        private OrderClass order;
        
        public Publisher(string s)
        {
            base_price = (c.Base_Price(c.Day())).Item1;
            discount = (c.Base_Price(c.Day()).Item2);

            Cipher cipher = new Cipher();
            order = cipher.decoder(s);


        }

        public double PricingModel()
        {
            double final_price = base_price * discount;
            if ( order.Amount > 10 )
                final_price = final_price - (order.Amount * .05);
            return final_price; 
        }
    }
}
