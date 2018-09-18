using System;
using System.Threading;

namespace Project2
{

    // Created by: Matthew Lillie on 09/05/2018
    class MultiCellBuffer
    {
        // Constants for the buffer
        private const int MAXIMUM_BUFFER_SIZE = 3;
        private static object BUFFER_LOCK = new object();

        //The necessary fields for the buffer
        private string[] buffer;
        private int currentBufferSize;
        private Semaphore semaphore;

        public MultiCellBuffer()
        {
            this.currentBufferSize = 0;
            this.semaphore = new Semaphore(MAXIMUM_BUFFER_SIZE, MAXIMUM_BUFFER_SIZE);
            this.buffer = new string[MAXIMUM_BUFFER_SIZE];
        }

        public void WriteToBuffer(string value)
        {
            Console.WriteLine("START WRITE");

            //Wait for a spot to be open
            semaphore.WaitOne();
            lock (BUFFER_LOCK) // Lock the buffer since we are now attempting to write to it
            {
                //Find the first value that is null in the buffer and set it to be the new value
                //Then increase the current size of the buffer
                for (int i = 0; i < MAXIMUM_BUFFER_SIZE; i++)
                {
                    if (buffer[i] == null)
                    {
                        buffer[i] = value;
                        currentBufferSize++;
                        break;
                    }
                }
            }

            Console.WriteLine("END WRTITE");
        }

        public string ReadFromBuffer()
        {
            string value = null;
            lock(BUFFER_LOCK) // Lock the buffer because we are now attempting to read from it
            {
                //Find the first non null value in the buffer and use that as the return value
                //Then decrement the current size of the buffer
                for (int i = 0; i < MAXIMUM_BUFFER_SIZE; i++)
                {
                    if (buffer[i] != null)
                    {
                        value = buffer[i];
                        buffer[i] = null;
                        currentBufferSize--;
                        semaphore.Release();
                        break;
                    }
                }
            }
            return value;
        }

        public Boolean isFull()
        {
            lock(BUFFER_LOCK)
            {
                return currentBufferSize == MAXIMUM_BUFFER_SIZE;
            }
        }
    }
}
