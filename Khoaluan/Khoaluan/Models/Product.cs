using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khoaluan.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }  
        public string Image { get; set; }
        public int DevId { get; set; }
        public Developer Developer { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Library> Libraries { get; set; }
        public List<Item> Items { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Refund> Refunds { get; set; }
    }
}
