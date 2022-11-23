using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Models
{
    public class Admin
    {
        public string TaiKhoan { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
