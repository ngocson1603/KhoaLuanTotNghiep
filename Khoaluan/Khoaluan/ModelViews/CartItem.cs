using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class CartItem
    {
        public Product product { get; set; }
        public int amount { get; set; }
    }
}
