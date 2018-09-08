using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    // Created by: Matthew Lillie on 09/05/2018
    class OrderObject
    {
        private int senderId; //The thread id of the sender 
        private int cardNo; //The credit card number used for the order
        private int receiverId;//The thread id of the receiver 
        private int amount;//The amount of books for the order
        private double unitPrice;//The price of a single book

        public int SenderId { get => senderId; set => senderId = value; }
        public int CardNo { get => cardNo; set => cardNo = value; }
        public int ReceiverId { get => receiverId; set => receiverId = value; }
        public int Amount { get => amount; set => amount = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
    }
}
