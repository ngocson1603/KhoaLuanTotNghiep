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
                var gh = HttpContext.Session.Get<List<Cart>>("GioHang");
                if (gh == default(List<Cart>))
                {
                    gh = new List<Cart>();
                }
                return gh;
            }
        }
        const string SessionName = "idpro";
        public List<int> lst = new List<int>();
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID)
        {
            List<Cart> cart = GioHang;
            List<int> list = new List<int> { 15,18,31 };
            try
            {
                //var add = new AddToCart() { ProID = productID };
                //HttpContext.Session.SetString("ADD",JsonConvert.SerializeObject(add));
                lst.Add(productID);
                TempData["idpro"] = list;
                //TempData["idpro"] = JsonConvert.DeserializeObject<AddToCart>(HttpContext.Session.GetString("ADD"));
                //Them san pham vao gio hang
                Cart item = cart.SingleOrDefault(p => p.product.Id == productID);
                if (item != null) 
                {
                    //luu lai session
                    TempData["idpro"] = lst;
                    HttpContext.Session.Set<List<Cart>>("GioHang", cart);
                }
                else
                {
                    Product hh = _unitOfWork.ProductRepository.GetById(productID);
                    item = new Cart
                    {
                        amount = 1,
                        product = hh
                    };
                    cart.Add(item);//Them vao gio
                }
                TempData.Keep("idpro");
                //Luu lai Session
                HttpContext.Session.Set<List<Cart>>("GioHang", cart);
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
                HttpContext.Session.Set<List<Cart>>("GioHang", gioHang);
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
