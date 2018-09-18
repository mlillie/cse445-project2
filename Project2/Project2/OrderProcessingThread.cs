using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class OrderProcessingThread
    {
        public static double tax = 0.083;
        public OrderProcessingThread()
        {
            //tax = .083;
            //We never actually instantiate this class so we can just set the tax up der ^
        }
        public static void OPT(OrderClass order)
        {
            if (order.CardNo < 1000 && order.CardNo > 100)
            {
                double total_amount = order.Unit_Price * order.Amount * tax;
                Console.WriteLine("Order: " + order.SenderId + "; " + order.ReceiverId + "; totaled $" + total_amount.ToString("0.00"));
            }
        }
    }
}