using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("Forum.html", Name = ("Forum"))]
        public IActionResult ForumInD()
        {
            return View();
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
