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

        public void BookstoreFunction()
        {
            while(Program.isRunning())
            {
                Thread.Sleep(random.Next(1000, 3000));
                string senderId = Thread.CurrentThread.Name;
                CreateBookOrder(senderId);
            }
        }

        public void CreateBookOrder(string senderId)
        {
            OrderObject order = new OrderObject();
            order.SenderId = senderId;
            //order.ReceiverId = receiverId; // This can be set when the publisher actually receivs the order
            order.Amount = random.Next(1, 5); //TODO amount has to change dynamically based on needs?
            order.CardNo = random.Next(100, 1000); //TODO?
            //order.UnitPrice = price; TODO set the price when the publisher recieves the order from the buffer so,
            // it can update the price counter and possibly change the price

            Console.WriteLine("Order has been created at {0} by sender {1}.", DateTime.Now.ToString("hh:mm:ss"), 
                senderId);

            //Encode and send to the buffer here
           // string encodedOrder = Encoding.encode(order);
            //buffer.WriteToBuffer(encodedOrder);
        }

        public void BookOnSale()
        {

        }
    }
}
