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
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("blogs.html", Name = ("Blog"))]
        public IActionResult BlogFull(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var ls = _unitOfWork.BlogRepository.GetAll();
            if (ls.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(ls.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = ls.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            ViewBag.CurrentPage = pageNumber;
            return View(plr);
        }
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinChiTiet")]
        public IActionResult BlogArticle(int id)
        {
            var tindang = _unitOfWork.BlogRepository.GetById(id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _unitOfWork.BlogRepository
                .GetAll()
                .Where(x => x.Published == true && x.Id != id)
                .Take(3)
                .OrderByDescending(x => x.CreatedDate).ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            return View(tindang);
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
