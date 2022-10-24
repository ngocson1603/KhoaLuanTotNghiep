using Khoaluan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class BlogController : Controller
    {
        //private readonly dbMarketsContext _context;
        //public BlogController(dbMarketsContext context)
        //{
        //    _context = context;
        //}

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult BlogFull(int? page)
        //{
        //    var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        //    var pageSize = 10;
        //    var lsTinDangs = _context.TinDangs
        //        .AsNoTracking()
        //        .OrderBy(x => x.PostId);
        //    PagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);

        //    ViewBag.CurrentPage = pageNumber;
        //    return View(models);
        //}
    }
}
