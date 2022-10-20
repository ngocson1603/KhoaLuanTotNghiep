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
        public int price { get; set; }  
        public string image { get; set; }
        public int DevId { get; set; }
        public Developer Developer { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
