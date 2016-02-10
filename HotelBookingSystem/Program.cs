using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace HotelBookingSystem
{
    
   class Program
    {
        
          
       public static void Main(string[] args)
        {
            HotelSupplier supplier1 = new HotelSupplier();
            HotelSupplier supplier2 = new HotelSupplier();
            
            try
            {
              //  Console.WriteLine("********************************************************\n");
                Console.WriteLine("                       Hotel Block Booking System");
                Console.WriteLine("               *************************************\n");
                Console.WriteLine("Setup: (1) 3 BufferCells  (2) 5 Travel Agency Threads (3) 2 Hotel Supplier Objects");
                Console.WriteLine("***********************************************************************************\n");
                Console.SetBufferSize(80, 800);
                
                Thread Supplier1 = new Thread(new ThreadStart(supplier1.priceCutFunction));   // Two objects for Hotel Suppliers are instantiated
                Thread Supplier2 = new Thread(new ThreadStart(supplier2.priceCutFunction));
                Supplier1.Name = "HotelSupplier1";   //setting a name to the thread
                Supplier2.Name = "HotelSupplier2";
                supplier1.priceCut += new HotelSupplier.PriceCutEvent(TravelAgency.roomDiscount);  // Event in HotelSupplier class and the event handler in the travel agency class are linked
                supplier2.priceCut += new HotelSupplier.PriceCutEvent(TravelAgency.roomDiscount);
                Thread[] travelAgency = new Thread[5];   //creating 5 retailer threads
                for (int i = 0; i < 5; i++)
                {
                    travelAgency[i] = new Thread(new ThreadStart(TravelAgency.travelFunc));    
                    travelAgency[i].Name = "Travel Agency " + (i + 1).ToString();     
                    travelAgency[i].IsBackground = false;
                    travelAgency[i].Start();     //startin the thread
                }
                 Supplier1.Start();    //starting the Supplier1 thread
                 Supplier2.Start();   //starting the Supplier2 thead
                  Supplier1.Join();
                  Supplier2.Join();
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Error occurred in main method\n" + e.Message);
            }
        }
    }
}
