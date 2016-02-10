using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    public static class Decoder
    {
        static object decodeLock = new object();

        public static OrderClass Decode(string encodeString)
        {
            try
            {
                lock (decodeLock)
                {
                    byte[] byteEncoded = Convert.FromBase64String(encodeString);
                    string[] decodedString = ASCIIEncoding.ASCII.GetString(byteEncoded).Split('#');
                    return new OrderClass(decodedString[0], Convert.ToInt32(decodedString[1]), Convert.ToInt32(decodedString[2]), Convert.ToDateTime(decodedString[3]));    //storing the decoded message into the order class object and returning it back to the calling method
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OrderClass("", 0, 0, Convert.ToDateTime(""));
            }
        }

    }
}
