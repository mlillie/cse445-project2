using System;
using System.Threading;

namespace Project2
{
    class Program
    {
        // We can pass this to the constructor of bookstore and publisher so they can use it
        private static MultiCellBuffer buffer = new MultiCellBuffer();

        private static Boolean running = true; // Necessary?

        //The publisher threads associated with the entire program.
        private static Thread[] publishers;

        //The bookstore threads associated with the entire program.
        private static Thread[] bookstores;

        //The number of bookstore threads
        private const int NUMBER_OF_BOOKSTORES = 5;

        //The number of publisher threads
        private const int NUMBER_OF_PUBLISHERS = 2;

        static void Main(string[] args)
        {
            //Create the bookstore and create all the threads associated with the bookstore
            BookStore bookstore = new BookStore(buffer);

            bookstores = new Thread[NUMBER_OF_BOOKSTORES];
            for (int i = 0; i < NUMBER_OF_BOOKSTORES; i ++)
            {
                bookstores[i] = new Thread(new ThreadStart(bookstore.BookstoreFunction));
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

        public static Boolean isRunning()
        {
            return running; 
        }
    }
}
