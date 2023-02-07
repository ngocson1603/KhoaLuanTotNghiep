using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Models
{
    public class Fund : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public float Tax { get; set; }
        public List<AddFundTransaction> AddFundTransactions { get; set; }
    }
}
