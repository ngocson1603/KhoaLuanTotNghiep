using System;

namespace Khoaluan.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Ad_Username { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Published { get; set; }
        public string Alias { get; set; }
        public string Contents { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Admin Admin { get; set; }
    }
}
