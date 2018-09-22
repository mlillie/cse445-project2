using System;
using System.Threading;

namespace Project2
{

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

        //Created by Matthew Lillie
        public void WriteToBuffer(string value)
        {
            lock (BUFFER_LOCK) // Lock the buffer since we are now attempting to write to it
            {
                //Wait for a spot to be open
                semaphore.WaitOne();

                //Wait while the buffer is full
                while (currentBufferSize == MAXIMUM_BUFFER_SIZE)
                {
                    Monitor.Wait(BUFFER_LOCK);
                }

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

                Monitor.Pulse(BUFFER_LOCK);
            }
        }

        // Created by Jacqueline Fonseca
        public string ReadFromBuffer()
        {
            string value = null;
            lock(BUFFER_LOCK) // Lock the buffer because we are now attempting to read from it
            {
                //Wait while the buffer is empty
                while (currentBufferSize == 0)
                {
                    Monitor.Wait(BUFFER_LOCK);
                }

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
                Monitor.Pulse(BUFFER_LOCK);
            }
            return value;
        }

        // Determines whether or not the buffer is currently full or not
        // Created by Jacqueline Fonseca/Matthew Lillie
        public Boolean IsFull()
        {
            lock(BUFFER_LOCK)
            {
                return currentBufferSize == MAXIMUM_BUFFER_SIZE;
            }
        }
    }
}
