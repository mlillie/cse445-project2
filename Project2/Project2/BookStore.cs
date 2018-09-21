using System;
using System.Threading;

namespace Project2
{
    class BookStore
    {

        //Random to be used for generating amounts, numbers, etc
        private Random random;

        //The buffer that is being passed to this by the main program
        private MultiCellBuffer buffer;

        //The calendar used for gather values.
        private Calendar calendar;

        //The total books to be sold.
        private int total_books;

        //The current amount of confirmed orders.
        private static int count_confirmations = 0;

        //This is for accessing the count_confirmations integer
        private static object COUNT_OBJECT_LOCK = new object();

        public BookStore(MultiCellBuffer buffer)
        {
            this.buffer = buffer;
            this.random = new Random();
            this.calendar = new Calendar();
            this.total_books = Convert.ToInt32((calendar.Base_Price(calendar.Day())).Item3);
        }

        public void BookstoreFunction()
        {
            while (true)
            {
                // Check to see if the publisher has finished with a sleep of 1-2.5 seconds
                if (Program._stopper.WaitOne(random.Next(1000, 2500), false))
                {
                    break;
                }

                // If the buffer is currently not full, create a book order
                if (!buffer.IsFull())
                {
                    string senderId = Thread.CurrentThread.Name;
                    CreateBookOrder(senderId);
                }
            }

            Console.WriteLine("TERMINATING: " + Thread.CurrentThread.Name);
        }

        public void CreateBookOrder(string senderId)
        {
            lock(COUNT_OBJECT_LOCK)
            {
                //Create an oorder object
                OrderClass order = new OrderClass();
                order.SenderId = senderId;

                //Calculate the number of books to be sold for this order
                int temp = total_books - count_confirmations;
                order.Amount = (temp / 15);
                total_books -= order.Amount;

                //Card number will be between 100 and 1000
                order.CardNo = random.Next(100, 1000); //TODO?

                // Place holders as these will be set once the publisher receives it 
                order.ReceiverId = "TODO";
                order.Unit_Price = 0.0;

                //Encode and send to the buffer here
                string encodedOrder = Cipher.encoder(order);
                buffer.WriteToBuffer(encodedOrder);
                Console.WriteLine("Order has been created at " + DateTime.Now.ToString("hh:mm:ss") + " by sender " + senderId + ".");
            }
  
        }

        public void BookOnSale(int publisherId, double price)
        {
            Console.WriteLine("A SALE IS HAPPENING FROM PUBLISHER #" + publisherId.ToString()
                + " WITH A NEW LOW PRICE OF: $" + price.ToString("0.00"));
        }

        public static void Confirmation(OrderClass order)
        {
            lock(COUNT_OBJECT_LOCK)
            {
                Console.Write("Order from " + order.ReceiverId + " with " + order.Amount + " book(s) has been approved.\n");
                count_confirmations++;
            }
        }

    }
}