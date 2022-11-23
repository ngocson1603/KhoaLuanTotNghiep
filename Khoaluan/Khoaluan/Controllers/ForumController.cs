using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.InterfacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscussionService _discussionService;
        public INotyfService _notyfService { get; }
        public ForumController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        ////[Route("/Forum/{id}.html", Name = ("Forum"))]
        //[Route("forum.html", Name = ("Forum"))]
        //public IActionResult ForumInD(int id)
        //{
        //    var ls1 = _dis.listDiscussion(id);
        //    return View(ls1);
        //}
        [Route("Forum.html", Name = ("Forum"))]
        public IActionResult ForumInD()
        {
            var ls1 = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(ls1);
        }
        // GET: ForumController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ForumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ForumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForumController/Edit/5
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

        // GET: ForumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForumController/Delete/5
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
        [Route("forum-single-topic.html", Name = ("DetailForum"))]
        public IActionResult DetailForum()
        {
            return View();
        }
    }
}
