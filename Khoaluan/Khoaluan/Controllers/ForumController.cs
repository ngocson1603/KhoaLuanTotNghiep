using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public ForumController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Forum.html", Name = ("Forum"))]
        public IActionResult ForumInD()
        {
            var ls1 = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(ls1);
        }
        [Route("forum-topics.html", Name = ("ForumList"))]
        public IActionResult ForumList()
        {
            return View();
        }
        [Route("forum-single-topic.html", Name = ("DetailForum"))]
        public IActionResult DetailForum()
        {
            return View();
        }
    }
}
