﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Khoaluan.Extension;
using Khoaluan.ModelViews;
using Khoaluan.Models;

namespace Khoaluan.Controllers.Components
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("GioHang");
            return View(cart);
        }
    }
}
