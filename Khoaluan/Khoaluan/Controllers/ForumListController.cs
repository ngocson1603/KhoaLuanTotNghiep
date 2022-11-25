using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class ForumListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public ForumListController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
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
        public ActionResult Create()
        {
            var list = _unitOfWork.DiscussionRepository.GetAll().Where(t => t.ProductID == idpro);
            return View(list);
        }

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
                _notyfService.Success("Thêm thành công");
                return Redirect(url);
            }
            _notyfService.Success("Đã có lỗi xảy ra");
            return View(discussion);
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
                _notyfService.Success("Sửa thành công");
                return Redirect(url);
            }
            _notyfService.Success("Đã có lỗi xảy ra");
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
                _notyfService.Success("Xóa quyền truy cập thành công");
                return Redirect(url);
            }
            catch
            {
                _notyfService.Success("Đã có lỗi xảy ra");
                return Redirect(url);
            }
        }
        public static int idpro;
        [Route("/forum-topics/{id}.html", Name = ("ForumList"))]
        public IActionResult ForumList(int id)
        {
            idpro = id;
            var list = _unitOfWork.DiscussionRepository.GetAll().Where(t => t.ProductID == id);
            return View(list);
        }
        [Route("forum-single-topic.html", Name = ("DetailForum"))]
        public IActionResult DetailForum()
        {
            return View();
        }
        [Route("Forum.html", Name = ("Forum"))]
        public IActionResult ForumInD()
        {
            var ls1 = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(ls1);
        }
    }
}
