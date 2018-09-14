using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    // Created by Jacqueline Fonseca on 09/08/2018
    // Changed senderId and receiverId to be strings
    class OrderClass
    {
        private int cardNo, amount;
        private string senderId, receiverId;
        private double unit_price;

        public string SenderId
        {
            get { return senderId; }
            set { senderId = value;  }
        }
        public int CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public double Unit_Price
        {
            get { return unit_price; }
            set { unit_price = value; }
        }
        public string ReceiverId
        {
            get { return receiverId; }
            set { receiverId = value; }
        }
    }
}
