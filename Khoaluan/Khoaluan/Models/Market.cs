using System;

namespace Khoaluan.Models
{
    public class Market:BaseEntity
    {
        public int UserID { get;set; }
        public int ItemID { get; set; }
        public double Price { get; set; }

        public DateTime DayCreate { get; set; }
        public int Status { get; set; }
        public Users User { get; set; }
        public Item Item { get; set; }
    }
}
