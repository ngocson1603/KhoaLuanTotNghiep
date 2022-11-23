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
        private readonly IService _service;
        private readonly IDiscussionRepository _discussionRepository;
        public INotyfService _notyfService { get; }
        public ForumListController(IDiscussionRepository discussionRepository, INotyfService notyfService)
        {
            //_unitOfWork = unitOfWork;
            _discussionRepository = discussionRepository;
            _notyfService = notyfService;
        }
        // GET: ForumListController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ForumListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumListController/Create
        public ActionResult Create()
        {
            var list = _discussionRepository.GetAll().Where(t => t.ProductID == idpro);
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
                _discussionRepository.Add(discussion1);
                //_unitOfWork.SaveChange();
                _notyfService.Success("Tạo mới thành công");
                return Redirect(url);/*RedirectToRoute(ForumList(idpro));*/
            }
            return View(discussion);
        }

        // GET: ForumListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForumListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForumListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForumListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public static int idpro;
        [Route("/forum-topics/{id}.html", Name = ("ForumList"))]
        public IActionResult ForumList(int id)
        {
            idpro = id;
            var list = _discussionRepository.GetAll().Where(t => t.ProductID == id);
            return View(list);
        }
        //[Route("forum-single-topic.html", Name = ("DetailForum"))]
        //public IActionResult DetailForum()
        //{
        //    return View();
        //}
    }
}
