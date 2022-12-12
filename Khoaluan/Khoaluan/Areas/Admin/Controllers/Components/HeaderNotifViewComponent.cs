using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Khoaluan.Extension;
using Khoaluan.ModelViews;
using Khoaluan.Models;

namespace Khoaluan.Areas.Admin.Controllers
{
    public class HeaderNotifViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public HeaderNotifViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            var pro = _unitOfWork.ProductRepository.listProdevNotif();
            return View(pro);
        }
    }
}
