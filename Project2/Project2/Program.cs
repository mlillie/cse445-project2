using System;
using System.Threading;

namespace Project2
{
    class Program
    {
        // We can pass this to the constructor of bookstore and publisher so they can use it
        private static MultiCellBuffer buffer = new MultiCellBuffer();

        public static Boolean isRunning = true; // Necessary?

        static void Main(string[] args)
        {
        }
    }
}
