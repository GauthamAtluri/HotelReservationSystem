using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HotelBookingSystem
{
    public class HotelSupplier
    {
        public delegate void PriceCutEvent(Int32 price);    //initializing the event which is used whena pricecut occurs.
        public  event PriceCutEvent priceCut;
        public  static Int32 roomPrice = 19;
        public static    Int32 prevRoomPrice = 19;
        public        Int32 count = 0;
        
        static object lockObject = new object();

        public  void priceCutFunction()     //processes the order and calls the order processing. Checks whether a price cut happened.
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    lock (lockObject)
                    {
                        if (count >= 10)  //checks whether the maximum price cuts occurred
                        {
                           // Console.ForegroundColor = ConsoleColor.Red;
                            //Console.Write("\n\nAlert: ");
                           // Console.ResetColor();
                            Console.WriteLine("Looks like the maximum price cuts have occured. Terminating the process for now!");
                            break;
                        }
                        string encodedMessage = MultiCellBuffer.GetOneCell();   //retreives a message from the multicellbuffer i.e, the order placed by the retailer
                        OrderClass orderObject = Decoder.Decode(encodedMessage); //decoding the message which is retreived from the multicelbuffer
                        Int32 newRoomPrice = PricingModel.priceCalculator(); //callng the pricingmodel which calculates the price of the chicken
                        changePrice(newRoomPrice);   //calls the changePrice method which changes the price if the new price of the chicken if it is less than the old price
                        Thread orderProcessingThread = new Thread(() => OrderProcessing.processOrder(orderObject, roomPrice));   //creating a order processing thread and instantiating process order with the order class object and new chicken price.
                        orderProcessingThread.Start();  //starting the thread
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There is some error which occured in the HotelSupplier Function. Please recheck! " + e.Message);
            }
        }

        public  void changePrice(Int32 newRoomPrice)   //this method changes the price of the chicken if the new generated price is less than the old price
        {
            if (newRoomPrice < roomPrice)     //checks the new price over the old price
            {
                count++;      //incrementing the counter of number of price cuts
                priceCut(newRoomPrice);  //generating a event

                prevRoomPrice = roomPrice;
                roomPrice = newRoomPrice;
            }
        }
    }
}
