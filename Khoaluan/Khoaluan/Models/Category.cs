using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
