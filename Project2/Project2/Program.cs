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


        static void Main(string[] args)
        {
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
