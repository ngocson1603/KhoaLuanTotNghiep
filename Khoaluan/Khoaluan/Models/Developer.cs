using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Developer:BaseEntity
    {
        public string Name { get; set; }    
        public List<Product> Products { get; set; }
    }
}
