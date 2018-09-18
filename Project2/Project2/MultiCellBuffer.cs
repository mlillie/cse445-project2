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
        private Semaphore writeSemaphore, readSemaphore;

        public MultiCellBuffer()
        {
            this.currentBufferSize = 0;
            this.writeSemaphore = new Semaphore(MAXIMUM_BUFFER_SIZE, MAXIMUM_BUFFER_SIZE);
            this.readSemaphore = new Semaphore(MAXIMUM_BUFFER_SIZE, MAXIMUM_BUFFER_SIZE);
            this.buffer = new string[MAXIMUM_BUFFER_SIZE];
        }

        public void WriteToBuffer(string value)
        {
            writeSemaphore.WaitOne();
            lock (BUFFER_LOCK) // Lock the buffer since we are now attempting to write to it
            {
                while (currentBufferSize == MAXIMUM_BUFFER_SIZE)
                {
                    //Console.WriteLine("Buffer is FULL");
                    Monitor.Wait(BUFFER_LOCK); // The buffer is at capacity so wait
                }

                //Find the first value that is null in the buffer and set it to be the new value
                //Then increase the current size of the buffer
                for(int i = 0; i < MAXIMUM_BUFFER_SIZE; i ++)
                {
                    if(buffer[i] == null)
                    {
                        buffer[i] = value;
                        currentBufferSize++;
                        break;
                    }
                }

                //Release the write semaphore
                writeSemaphore.Release();
                //Allow the lock to be used elsewhere
                Monitor.Pulse(BUFFER_LOCK);
            }
        }

        public string ReadFromBuffer()
        {

            readSemaphore.WaitOne();
            string value = null;
            lock(BUFFER_LOCK) // Lock the buffer because we are now attempting to read from it
            {
                while (currentBufferSize == 0)
                {
                    //Console.WriteLine("Buffer is EMPTY");
                    Monitor.Wait(BUFFER_LOCK); // The buffer is empty so wait
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
                        break;
                    }
                }
                
                //Release the read semaphore 
                readSemaphore.Release();
                //Allow the lock to be used elsewhere
                Monitor.Pulse(BUFFER_LOCK);
            }
            return value;
        }

    }
}
