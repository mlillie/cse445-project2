using System;
using System.Threading;

namespace Project2
{
    class Program
    {

        //Moved it to here
        public delegate void PriceCutEvent(int publisherId, double newPrice);

        // We can pass this to the constructor of bookstore and publisher so they can use it
        private static MultiCellBuffer buffer;

        //The publisher threads associated with the entire program.
        private static Thread[] publishers;

        //The bookstore threads associated with the entire program.
        private static Thread[] bookstores;

        //The number of bookstore threads
        private const int NUMBER_OF_BOOKSTORES = 5;

        //The number of publisher threads
        private const int NUMBER_OF_PUBLISHERS = 2;

        private static Random random = new Random();

        private static Boolean[] publishersRunning;

        public static ManualResetEvent _stopper = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            publishersRunning = new Boolean[NUMBER_OF_PUBLISHERS];

            buffer = new MultiCellBuffer();

            Publisher publisher;
            BookStore bookstore = new BookStore(buffer);

            Publisher.priceCut += new PriceCutEvent(bookstore.BookOnSale);

            publishers = new Thread[NUMBER_OF_PUBLISHERS];
            for (int i = 0; i < NUMBER_OF_PUBLISHERS; i++)
            {
                publisher = new Publisher(buffer, i);
                publishers[i] = new Thread(new ThreadStart(publisher.PublisherFunction));
                publishers[i].Name = "Publisher #" + i.ToString();
                publishers[i].Start();
                publishersRunning[i] = true;
            }

            bookstores = new Thread[NUMBER_OF_BOOKSTORES];
            for (int i = 0; i < NUMBER_OF_BOOKSTORES; i ++)
            {
                bookstores[i] = new Thread(new ThreadStart(bookstore.BookstoreFunction));
                bookstores[i].Name = "Bookstore #" + i.ToString();
                bookstores[i].Start();
            }


        }

        public static Thread[] getPublishers()
        {
            return publishers;
        }

        public static Thread[] getBookstores()
        {
            return bookstores;
        }

        private static Boolean isRunning()
        {
            for (int i = 0; i < NUMBER_OF_PUBLISHERS; i++)
            {
                if (publishersRunning[i])
                    return true;
            }
            return false;
        }

        public static void setRunning(int publisherId, Boolean value)
        {
            publishersRunning[publisherId] = value;
            if(!isRunning())
            {
                _stopper.Set();
            }
        }
    }
}
