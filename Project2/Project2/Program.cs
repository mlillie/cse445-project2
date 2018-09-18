using System;
using System.Threading;

namespace Project2
{
    class Program
    {
        // We can pass this to the constructor of bookstore and publisher so they can use it
        private static MultiCellBuffer buffer;

        private static Boolean running = true; // Necessary?

        //The publisher threads associated with the entire program.
        private static Thread[] publishers;

        //The bookstore threads associated with the entire program.
        private static Thread[] bookstores;

        //The number of bookstore threads
        private const int NUMBER_OF_BOOKSTORES = 5;

        //The number of publisher threads
        private const int NUMBER_OF_PUBLISHERS = 2;

        private static Random random = new Random();

        static void Main(string[] args)
        {
            buffer = new MultiCellBuffer();
            //Create the publisher and create all the threads associated with the publisher
            Publisher publisher = new Publisher(buffer);

            publishers = new Thread[NUMBER_OF_PUBLISHERS];
            for (int i = 0; i < NUMBER_OF_PUBLISHERS; i++)
            {
                publishers[i] = new Thread(new ThreadStart(publisher.PublisherFunction));
                publishers[i].Name = "Publisher #" + i.ToString();
                publishers[i].Start();
            }

            //Create the bookstore and create all the threads associated with the bookstore
            BookStore bookstore = new BookStore(buffer);

            bookstores = new Thread[NUMBER_OF_BOOKSTORES];
            for (int i = 0; i < NUMBER_OF_BOOKSTORES; i ++)
            {
                bookstores[i] = new Thread(new ThreadStart(bookstore.BookstoreFunction));
                bookstores[i].Name = "Bookstore #" + i.ToString();
                bookstores[i].Start();
            }


        }

        private static void WriteToBuffer()
        {
            while(running)
            {
                Thread.Sleep(250);
                string val = "Writing: "+ random.NextDouble().ToString() + " value";
                buffer.WriteToBuffer(val);
                Console.WriteLine(val);
            }
        }

        private static void ReadFromBuffer()
        {
            while (running)
            {
                Thread.Sleep(random.Next(500, 3500));
                string val = "Reading: " + buffer.ReadFromBuffer() + " value";
                Console.WriteLine(val);
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

        public static void setRunning(Boolean val)
        {
            running = val;
        }
    }
}
