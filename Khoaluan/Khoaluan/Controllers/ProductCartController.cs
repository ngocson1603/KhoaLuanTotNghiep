using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan;
using Khoaluan.Extension;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }
        public static int idpro;
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID)
        {
            List<CartItem> cart = GioHang;
            //ViewBag.listpro = cart;
            try
            {
                //Them san pham vao gio hang
                CartItem item = cart.SingleOrDefault(p => p.product.Id == productID);
                if (item != null) // da co => cap nhat so luong
                {
                    //luu lai session
                    ViewBag.idpro = productID;
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                else
                {
                    Product hh = _unitOfWork.ProductRepository.GetAll().SingleOrDefault(p => p.Id == productID);
                    item = new CartItem
                    {
                        amount = 1,
                        product = hh
                    };
                    cart.Add(item);//Them vao gio
                }

                //Luu lai Session
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
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
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.product.Id == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                //luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
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
            return View(GioHang);
        }
    }
}
