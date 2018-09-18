﻿using System;
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
            while (true)
            {
                if (Program._stopper.WaitOne(random.Next(1000, 2500), false))
                {
                    break;
                }
                if(!buffer.isFull())
                {
                    string senderId = Thread.CurrentThread.Name;
                    CreateBookOrder(senderId);
                }
            }

            Console.WriteLine("TERMINATING: " + Thread.CurrentThread.Name);
        }

        public void CreateBookOrder(string senderId)
        {
            OrderClass order = new OrderClass();
            order.SenderId = senderId;
            //order.ReceiverId = receiverId; // This can be set when the publisher actually receivs the order
            order.Amount = random.Next(1, 5); //TODO amount has to change dynamically based on needs?
            order.CardNo = random.Next(100, 1000); //TODO?
            order.ReceiverId = "TODO";
            order.Unit_Price = 0.0;
            //order.UnitPrice = price; TODO set the price when the publisher recieves the order from the buffer so,
            // it can update the price counter and possibly change the price

            //Encode and send to the buffer here
           string encodedOrder = Cipher.encoder(order);
           Console.WriteLine("Before write buffer: " + Thread.CurrentThread.Name);
           buffer.WriteToBuffer(encodedOrder);
           Console.WriteLine("Order has been created at " + DateTime.Now.ToString("hh:mm:ss") + " by sender " + senderId + ".");
        }

        public void BookOnSale(int publisherId, double price)
        {
            Console.WriteLine("A SALE IS HAPPENING FROM PUBLISHER #"+ publisherId.ToString() 
                +" WITH A NEW LOW PRICE OF: $" + price.ToString("0.00"));
        }
    }
}
