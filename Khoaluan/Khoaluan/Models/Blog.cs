namespace Khoaluan.Models
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Ad_Username { get; set; }
        public Admin Admin { get; set; }
    }
}
