using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Areas.Admin.Models
{
    public class MultiDropDownListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> ItemList { get; set; }
    }
}
