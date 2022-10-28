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
    }
}
