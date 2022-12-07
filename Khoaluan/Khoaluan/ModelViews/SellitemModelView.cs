using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class SellitemModelView
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int PricePerItem { get; set; }
        public int Quantity { get; set; }
        public string NameItem { get; set; }
        public DateTime DayCreate { get; set; }
        public int UserId { get; set; }
        public string HoTen { get; set; }
        public string NameGame { get; set; }
        public int Count { get; set; }
    }
}
