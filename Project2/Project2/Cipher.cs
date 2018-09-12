using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    // Created by Jacqueline Fonseca on 09/08/2018

    class Cipher
    {
        public string encoder(OrderClass orderObject)
        {
            return "";
        }
            
        public OrderClass decoder(string orderString)
        {
            OrderClass orderObject = new OrderClass();
            string[] parsed = orderString.Split(',');

            orderObject.SenderId = Convert.ToInt32(parsed[1]);
            orderObject.CardNo = Convert.ToInt32(parsed[2]);
            orderObject.ReceiverID = Convert.ToInt32(parsed[3]);
            orderObject.Amount = Convert.ToInt32(parsed[4]);
            orderObject.Unit_Price = Convert.ToDouble(parsed[5]);
            return orderObject;
        }
    }
}
