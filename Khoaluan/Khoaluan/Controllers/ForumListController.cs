using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khoaluan.Extension;
using System.Text.RegularExpressions;
using PagedList.Core;
using Khoaluan.Enums;

namespace Khoaluan.Controllers
{
    public class ForumListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public IDiscussionService _discussionService;
        public ForumListController(IUnitOfWork unitOfWork, INotyfService notyfService, IDiscussionService discussionService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _discussionService = discussionService;
        }
        // GET: ForumListController
        public ActionResult Index()
        {
            return View();
        }
        [Route("mydiscuss.html", Name = "DashboardDiscuss")]
        public IActionResult DashboardDiscuss()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.DiscussionRepository.GetAll().Where(x => x.UserName == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    return View(khachhang);
                }
            }
            return Redirect("dang-nhap.html");
        }
        // GET: ForumListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumListController/Create

        // POST: ForumListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DiscussionViewModel discussion)
        {
            if (ModelState.IsValid)
            {
                string url = "/forum-topics/"+idpro+ ".html";
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                Discussion discussion1 = new Discussion
                {
                    Title = discussion.Title,
                    Content = discussion.Content,
                    CreateDate = DateTime.Now,
                    ProductID = idpro,
                    Name = User.Identity.Name,
                    UserName = int.Parse(taikhoanID),
                };
                _unitOfWork.DiscussionRepository.Add(discussion1);
                _notyfService.Success("Add success");
                return Redirect(url);
            }
            _notyfService.Warning("An error has occurred");
            return RedirectToAction("ForumInD");
        }

        // GET: ForumListController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discuss = _unitOfWork.DiscussionRepository.GetById(id);
            if (discuss == null)
            {
                return NotFound();
            }

            return View(discuss);
        }

        // POST: ForumListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, DiscussionViewModel discussion)
        {
            if (ModelState.IsValid)
            {
                string url = "/mydiscuss.html";
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                Discussion discussion1 = new Discussion
                {
                    postID = id,
                    Title = discussion.Title,
                    Content = discussion.Content,
                    CreateDate = discussion.CreateDate,
                    ProductID = discussion.ProductID,
                    Name = User.Identity.Name,
                    UserName = int.Parse(taikhoanID),
                };
                _unitOfWork.DiscussionRepository.Update(id,discussion1);
                //_unitOfWork.SaveChange();
                _notyfService.Success("Successful fix");
                return Redirect(url);
            }
            _notyfService.Success("An error has occurred");
            return View(discussion);
        }

        // GET: ForumListController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discuss = _unitOfWork.DiscussionRepository.GetById(id);
            if (discuss == null)
            {
                return NotFound();
            }

            return View(discuss);
        }

        // POST: ForumListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string url = "/mydiscuss.html";
            try
            {
                _unitOfWork.DiscussionRepository.Remove(id);
                _notyfService.Success("Delete discuss successfully");
                return Redirect(url);
            }
            catch
            {
                _notyfService.Success("An error has occurred");
                return Redirect(url);
            }
        }
        public static int idpro;
        [Route("/forum-topics/{id}.html", Name = ("ForumList"))]
        public IActionResult ForumList(int id,int? page)
        {
            idpro = id;
            ViewBag.idforum = id;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var list = _unitOfWork.DiscussionRepository.GetAll().Where(t => t.ProductID == id);
            if (list.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(list.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = list.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            ViewBag.CurrentPage = pageNumber;
            return View(plr);
        }
        public static string idpost;
        [Route("forum-single-topic/{id}.html", Name = ("DetailForum"))]
        public IActionResult DetailForum(string id, int? page)
        {
            idpost = id;
            Discussion model = _unitOfWork.DiscussionRepository.GetById(id);
            return View(model);
        }

        //[HttpPost]
        //public IActionResult PostComment(string postId, string message)
        //{
        //    _discussionService.comment(postId, "Test user", message);
        //    return RedirectToAction("DetailForum", new { id = postId });
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comment(CommentViewModel comment)
        {
            string name = User.Identity.Name;
            string comm = comment.Message;
            string url = "/forum-single-topic/" + idpost + ".html";

            if(comment.Message != null)
            {
                if (comm.Contains("img") && comm.Contains("style"))
                {
                    comm = Helpper.Utilities.SetSizeImage(comm);
                }

                try
                {
                    _discussionService.comment(idpost, name, comm);
                    _notyfService.Success("Successful new creation");
                    return Redirect(url);
                }
                catch
                {
                    _notyfService.Error("New creation failed");
                    return Redirect(url);
                }
            }
            else
            {
                _notyfService.Error("New creation failed");
                return Redirect(url);
            }
            
        }
        [Route("Forum.html", Name = ("Forum"))]
        public IActionResult ForumInD(int? page)
        {
            int release = (int)productType.release;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var ls1 = _unitOfWork.ProductRepository.listProforum().Where(t=>t.Status == release && t.ReleaseDate <= DateTime.Now).ToList();
            if (ls1.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(ls1.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = ls1.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            ViewBag.CurrentPage = pageNumber;
            return View(plr);
        }
    }
}
