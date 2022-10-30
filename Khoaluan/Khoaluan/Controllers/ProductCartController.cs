using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan;
using Khoaluan.Extension;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnGame.Controllers
{
    public class ProductCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public ProductCartController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        
        public List<Cart> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<Cart>>("_GioHang");
                if (gh == default(List<Cart>))
                {
                    gh = new List<Cart>();
                }
                return gh;
            }
        }
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID)
        {
            List<Cart> cart = GioHang;
            try
            {
                //HttpContext.Session.SetString("idpro1", JsonConvert.SerializeObject(productID));
                //Them san pham vao gio hang
                Cart item = cart.SingleOrDefault(p => p.product.Id == productID);
                if (item != null) 
                {
                    //luu lai session
                    //TempData["idpro"] = cart;
                    HttpContext.Session.Set<List<Cart>>("_GioHang", cart);
                }
                else
                {
                    Product hh = _unitOfWork.ProductRepository.GetById(productID);
                    item = new Cart
                    {
                        product = hh
                    };
                    cart.Add(item);//Them vao gio
                    HttpContext.Session.SetString("idpro1", JsonConvert.SerializeObject(productID));
                    var taikhoanID = HttpContext.Session.GetString("idpro1");

                    TempData["idpro"] = taikhoanID;
                    
                }
                TempData.Keep("idpro");
                //Luu lai Session
                HttpContext.Session.Set<List<Cart>>("_GioHang", cart);
                ViewBag.GioHang = cart;
                _notyfService.Success("Thêm sản phẩm thành công");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [Route("api/cart/remove")]
        public ActionResult Remove(int productID)
        {

            try
            {
                List<Cart> gioHang = GioHang;
                Cart item = gioHang.SingleOrDefault(p => p.product.Id == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                //luu lai session
                HttpContext.Session.Set<List<Cart>>("_GioHang", gioHang);
                var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
                ViewBag.GioHang = cart;
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [Route("cart.html", Name = "Cart")]
        public ActionResult Cart()
        {
            TempData.Keep("idpro");
            return View(GioHang);
        }
    }
}
