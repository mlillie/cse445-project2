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
        
        // Created by Matthew Lillie
        public static string encoder(OrderClass orderObject)
        {
            string encodedString = "";

            encodedString += orderObject.SenderId;
            encodedString += ",";
            encodedString += orderObject.CardNo.ToString();
            encodedString += ",";
            encodedString += orderObject.ReceiverId;
            encodedString += ",";
            encodedString += orderObject.Amount.ToString();
            encodedString += ",";
            encodedString += orderObject.Unit_Price.ToString();

            return encodedString;
        }

        // Created by Jacqueline Fonseca
        public static OrderClass decoder(string orderString)
        {
            OrderClass orderObject = new OrderClass();
            string[] parsed = orderString.Split(',');

            orderObject.SenderId = parsed[1];
            orderObject.CardNo = Convert.ToInt32(parsed[2]);
            orderObject.ReceiverId = parsed[3];
            orderObject.Amount = Convert.ToInt32(parsed[4]);
            orderObject.Unit_Price = Convert.ToDouble(parsed[5]);
            return orderObject;
        }
    }
}
