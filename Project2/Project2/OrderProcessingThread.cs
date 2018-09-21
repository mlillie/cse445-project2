using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class OrderProcessingThread
    {
        // Tax for final amount
        public static double tax = 0.0825;


        public static void OPT(OrderClass order)
        {
            // Checks whether card number is valid
            if (order.CardNo < 1000 && order.CardNo > 100)
            {
                double taxed = order.Unit_Price * tax;
                double total_amount = (taxed + order.Unit_Price) * order.Amount;
                Console.WriteLine("Order: " + order.SenderId + "; " + order.ReceiverId + "; totaled $" + total_amount.ToString("0.00"));

                // Sends confirmation to bookstore
                Project2.BookStore.Confirmation(order);
            }
        }
    }
}