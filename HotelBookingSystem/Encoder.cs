using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    public class Encoder
    {
        static object encodeLock = new object();

        public static string Encode(OrderClass orderObject)
        {
            try
            {
                lock (encodeLock)
                {
                    String message = orderObject.GetSenderId().ToString() + "#" + orderObject.GetCardNo().ToString() + "#" + orderObject.GetAmount().ToString() + "#" + orderObject.orderStartTime.ToString();  //separating each entity by @ symbol so that when we decode it back it helps to separate the entities.
                    byte[] encodebytes = ASCIIEncoding.ASCII.GetBytes(message);
                    return Convert.ToBase64String(encodebytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }
        }
    }
}
