using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class ActiveGame
    {
        public int Id { get; set; }
        public string NamePro { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string NameDev { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
