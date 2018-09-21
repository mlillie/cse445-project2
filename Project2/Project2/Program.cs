using System;
using System.Threading;

namespace Project2
{
    class Program
    {

        //Moved it to here
        public delegate void PriceCutEvent(int publisherId, double newPrice);

        // We can pass this to the constructor of bookstore and publisher so they can use it
        private static MultiCellBuffer buffer = new MultiCellBuffer();

        // A manual reset event used for stopping the bookstore when the publishers have all finished
        public static ManualResetEvent _stopper = new ManualResetEvent(false);

        //The publisher threads associated with the entire program.
        private static Thread[] publishers;

        //The bookstore threads associated with the entire program.
        private static Thread[] bookstores;

        //The number of bookstore threads
        private const int NUMBER_OF_BOOKSTORES = 5;

        //The number of publisher threads
        private const int NUMBER_OF_PUBLISHERS = 2;

        // A boolean array of the current amount of publishers running
        private static Boolean[] publishersRunning;

        static void Main(string[] args)
        {
            // Create bookstore object
            BookStore bookstore = new BookStore(buffer);

            // Add the handler for the price cut event
            Publisher.priceCut += new PriceCutEvent(bookstore.BookOnSale);

            // Creating and starting the publisher threads
            Publisher publisher;
            publishersRunning = new Boolean[NUMBER_OF_PUBLISHERS];
            publishers = new Thread[NUMBER_OF_PUBLISHERS];
            for (int i = 0; i < NUMBER_OF_PUBLISHERS; i++)
            {
                publisher = new Publisher(buffer, i);
                publishers[i] = new Thread(new ThreadStart(publisher.PublisherFunction));
                publishers[i].Name = "Publisher #" + i.ToString();
                publishers[i].Start();
                publishersRunning[i] = true;
            }

            // Creates and starting the bookstore threads
            bookstores = new Thread[NUMBER_OF_BOOKSTORES];
            for (int i = 0; i < NUMBER_OF_BOOKSTORES; i ++)
            {
                bookstores[i] = new Thread(new ThreadStart(bookstore.BookstoreFunction));
                bookstores[i].Name = "Bookstore #" + i.ToString();
                bookstores[i].Start();
            }

        }

        // Checks to see if all the publisher threads are still running
        private static Boolean isRunning()
        {
            for (int i = 0; i < NUMBER_OF_PUBLISHERS; i++)
            {
                if (publishersRunning[i])
                    return true;
            }
            return false;
        }

        //Sets a publishers running value to the given boolean value
        public static void setRunning(int publisherId, Boolean value)
        {
            publishersRunning[publisherId] = value;
            // If all the publishers have stopped then set the manual reset event to stop the bookstore thread
            if(!isRunning())
            {
                _stopper.Set();
            }
        }
    }
}
