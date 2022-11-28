using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Models
{

    public class Users: BaseEntity
    {
        public string Gmail { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }
        public string Salt { get; set; }
        public int Balance { get; set; }
        public List<Library> Libraries { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<Order> Orders { get; set; }
        public List<Market> Markets { get; set; }
        public List<Refund> Refunds { get; set; }
        public List<MarketTransaction> Buys { get; set; }
        public List<MarketTransaction> Sells { get; set; }
    }
}
