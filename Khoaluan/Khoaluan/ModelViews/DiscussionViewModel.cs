using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class DiscussionViewModel
    {
        [Required(ErrorMessage = ("Vui lòng nhập Title"))]
        [Display(Name = "Địa chỉ Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = ("Vui lòng nhập Content"))]
        [Display(Name = "Địa chỉ Content")]
        public string Content { get; set; }
        public int ProductID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
