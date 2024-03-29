﻿using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Item:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public Product Product { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<Market> Markets { get; set; }
        public List<MarketTransaction> MarketTransactions { get; set; }
    }
}
