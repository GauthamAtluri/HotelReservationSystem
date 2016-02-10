using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    class OrderProcessing
    {
        static object lockObject = new object();
        static int tax = 12;
        static int shippingHandlingCharges = 100;
        public static void processOrder(OrderClass orderObj, int roomPrice)
        {
            try
            {
                lock (lockObject)
                {
                    if (orderObj.GetCardNo() >= 5000 && orderObj.GetCardNo() <= 7000) //checking the card number is valid or not
                    {
                        int total = roomPrice * orderObj.GetAmount() + tax + shippingHandlingCharges;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Order Processing...Please Wait");
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("{0} placed a order for {1} rooms. Total Price = ${2}\n , {3} ", orderObj.GetSenderId(), orderObj.GetAmount(), total,roomPrice);
                        Console.ResetColor();
                        TravelAgency.orderConfirmation(orderObj);    //Calling the callback method of the retailer and sending order class object with it which contains the details of the processed order
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Error occurred in process order method " + e.Message);
            }
        }
    }
}
