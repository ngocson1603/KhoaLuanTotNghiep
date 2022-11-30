namespace Khoaluan.Models
{
    public class Inventory
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public virtual Users User { get; set; }
        public virtual Item Item { get; set; }
    }
}
