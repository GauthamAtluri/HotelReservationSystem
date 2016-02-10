using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HotelBookingSystem
{
    public static class MultiCellBuffer
    {
        const int BUFFER_SIZE = 3;
        static string[] buffer = new string[BUFFER_SIZE];
        static int write = -1;
        static int read = -1;
        static object lockobj = new object();
        static Semaphore bigSem = new Semaphore(2, 2); 
        static Semaphore smallSem = new Semaphore(0, 2);

        public static void SetOneCell(string encodeString)
        {
            try
            {
                bigSem.WaitOne();
                lock (lockobj)
                {
                    write = (write + 1) % BUFFER_SIZE;
                    buffer[write] = encodeString;
                }
                smallSem.Release();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetOneCell()
        {
            string decodeString = string.Empty;
            try
            {
                smallSem.WaitOne();
                lock (lockobj)
                {
                    read = (read + 1) % BUFFER_SIZE;
                    decodeString = buffer[read];
                }
                bigSem.Release();
                return decodeString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
       

    
}
}
