using System;
using System.Threading;

namespace Project2
{
    class BookStore
    {

        //TODO before sending the order to the MultiCellBuffer, a
        //time stamp must be saved. When the confirmation of order completion is received, the time of the order
        //will be calculated and saved
        //DateTime.Now.ToString("hh:mm:ss")
        //^ Done?
        
        //Random to be used for generating amounts, numbers, etc
        private static Random random = new Random();

        //The buffer that is being passed to this by the main program
        private MultiCellBuffer buffer;

        public BookStore(MultiCellBuffer buffer)
        {
            this.buffer = buffer;
        }

        public void CreateBookOrder(int senderId, int receiverId, double price)
        {
            OrderObject order = new OrderObject();
            order.SenderId = senderId;
            order.ReceiverId = receiverId;
            order.Amount = random.Next(1, 5); //TODO amount has to change dynamically based on needs?
            order.CardNo = random.Next(100, 1000); //TODO?
            order.UnitPrice = price;

            Console.WriteLine("Order has been created at {0} by sender {1}.", DateTime.Now.ToString("hh:mm:ss"), 
                senderId);

            //Encode and send to the buffer here
           // string encodedOrder = Encoding.encode(order);
            //buffer.WriteToBuffer(encodedOrder);
        }
    }
}
