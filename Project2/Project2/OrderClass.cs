using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    // Created by Jacqueline Fonseca on 09/08/2018
    class OrderClass
    {
        private int senderId, cardNo, amount, receiverID;
        private double unit_price;

        public int SenderId
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
        public int ReceiverID
        {
            get { return receiverID; }
            set { receiverID = value; }
        }
    }
}
