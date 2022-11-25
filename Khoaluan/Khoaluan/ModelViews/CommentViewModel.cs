using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = ("Vui lòng nhập Message"))]
        [Display(Name = "Địa chỉ Message")]
        public string Message { get; set; }
    }
}
