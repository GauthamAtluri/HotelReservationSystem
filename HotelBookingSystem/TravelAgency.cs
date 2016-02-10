using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelBookingSystem
{/*This class acts as a server and processes the order placed by the retailer and calls the orderprocessing method.
     * In this class a event is generated when a price cut occurres and calls the event handler method in the retailer*/
    class TravelAgency
    {
        static  object obj = new object();                //creating an object for lock
        static  Random randRoom = new Random();        // random number of rooms are generated
        static  Random randCardNo = new Random();              // random cardNo is generated
        static Int32  roomNumber;
       
        /*The retailer threads are placing their order within this method. 
        This class sends the order details to the encoder to encode the message and gets back the encoded message.
        The encoded message is placed within the multicellbuffer from which the server take this request and processes it.*/
        public static void travelFunc()
        {
            try
            {
                while (true)
                {
                    lock (obj)
                    {
                        if (HotelSupplier.prevRoomPrice < HotelSupplier.roomPrice)  //deciding the number of chickens to order based on the old and new price of the chicken.
                            roomNumber = randRoom.Next(30, 60);     //if old chicken price is less than new chicken price then less number of chickens will be ordered.
                        else
                            roomNumber = randRoom.Next(60, 90);

                        Int32 cardNo = randCardNo.Next(5000, 7000);           //random generation of card number
                        OrderClass orderObject = new OrderClass(Thread.CurrentThread.Name, cardNo, roomNumber, DateTime.Now); //creating a object of order class and sending the input parameters
                        string message = Encoder.Encode(orderObject);        //passing the order class object to the encoder decoder class to encode the data
                        MultiCellBuffer.SetOneCell(message);                        //placing the encoded message in the multi cell buffer
                        Console.WriteLine("Order Request being sent..Please Wait");
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("{0} requested an order of {1} rooms   \n", Thread.CurrentThread.Name, roomNumber);
                    }
                    Thread.Sleep(3000);         //making the thread to sleep so that all the thread requests are getting processed
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Error occurred in TravelAgency Function " + e.Message);
            }
        }
        /*Event handler for the priceCut event generated in the chickenfarm class.
        Whenever there is a price cut this method is called and displays to the retailer that a price cut happened*/
        public static void roomDiscount(int roomPrice)
        {
            try
            {
                Int32 roomPriceOld = HotelSupplier.roomPrice;
               // Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("FLASH! FLASH! Limited Time offer ! TRAVEL AGENCIES ARE YOU LISTENING?");
                Console.WriteLine("                                                 ");
                Console.WriteLine("{0} offers a price cut from ${1} to ${2} ", Thread.CurrentThread.Name, roomPriceOld, roomPrice);
               // string S = Thread.CurrentThread.Name;
                Console.WriteLine("                                                 ");
              //  Console.WriteLine("-----------------End of Message------------------\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine("Please Check.. error occured in the event handler " + e.Message);
            }
        }

        /*This method displays the order receipt to the retailer with all the details of the order.
         * It also displays the time taken to process a particular order*/
        public static void orderConfirmation(OrderClass orderObject)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                
                Console.WriteLine("Order Receipt being generated.. \n");
                Console.WriteLine("----------------------------------");
                Console.ResetColor();
                Console.WriteLine("Time taken for the order to be processed  = {0} seconds\n", System.Math.Round((DateTime.Now - orderObject.orderStartTime).TotalSeconds, 2)); //calculating the time taken for each order and displaying it
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Error occurred during order confirmation " + e.Message);
            }
        }
    }
}
