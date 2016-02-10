using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    class PricingModel
    {
        static Random priceChange = new Random();
        public static int priceCalculator()
        {
            try
            {
                return priceChange.Next(1, 20);    //takes a random price and returns it to the calling function
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Error occurred in main method " + e.Message);
                return 0;
            }
        }
    }
}
