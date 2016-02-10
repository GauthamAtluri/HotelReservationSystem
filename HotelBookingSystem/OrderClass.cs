using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem
{
    public class OrderClass
    {
        public DateTime orderStartTime { get; set; }
        private string senderId;
        private Int32 cardNo;
        private Int32 amount;

        public OrderClass(string senderId, Int32 cardNo, Int32 amount, DateTime orderStartTime)
        {
            this.senderId = senderId;
            this.cardNo = cardNo;
            this.amount = amount;
            this.orderStartTime = orderStartTime;
        }
        public string GetSenderId()
        {
            return senderId;
        }
        public void SetSenderId(string senderId)
        {
            this.senderId = senderId;
        }

        public Int32 GetCardNo()
        {
            return cardNo;
        }
        public void SetCardNo(Int32 cardNo)
        {
            this.cardNo = cardNo;
        }

        public Int32 GetAmount()
        {
            return amount;
        }
        public void SetAmount(Int32 amount)
        {
            this.amount = amount;
        }

    }
}
