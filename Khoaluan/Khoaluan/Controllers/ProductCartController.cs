using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan;
using Khoaluan.Enums;
using Khoaluan.Extension;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Authorization;
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
                //Them san pham vao gio hang
                Cart item = cart.SingleOrDefault(p => p.product.Id == productID);
                if (item != null) 
                {
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
                }
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
        [Authorize, HttpPost]
        public IActionResult ThanhToan()
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.Get<List<Cart>>("_GioHang");
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                var maKH = _unitOfWork.UserRepository.GetById(int.Parse(taikhoanID));
                var totalprice = cart.Sum(t => t.product.Price);
                int type = (int)marketType.buy;
                if (maKH.Balance > totalprice)
                {
                    _notyfService.Success("Số tiền không đủ");
                    return RedirectToRoute("Cart");
                }
                try
                {
                    var item = _unitOfWork.OrderRepository.createOrder(int.Parse(taikhoanID.ToString()), cart);
                    _unitOfWork.OrderRepository.Create(item);
                    _unitOfWork.UserRepository.updateBalance(int.Parse(taikhoanID.ToString()), totalprice, type);
                    _unitOfWork.SaveChange();
                    HttpContext.Session.Remove("_GioHang");
                    return Redirect("/ProductCart/CheckoutSuccess");
                }
                catch (Exception ex)
                {
                    //log
                    return Redirect("/ProductCart/CheckoutFail");
                }

            }
            return RedirectToRoute("Cart");
        }
        public IActionResult CheckoutFail()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Chưa thanh toán"
            //Xóa session
            return View();
        }

        public IActionResult CheckoutSuccess()
        {
            //Tạo đơn hàng trong database với trạng thái thanh toán là "Paypal" và thành công
            //Xóa session
            return View();
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

        public void GetLibrary()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            ViewBag.GetId = taikhoanID;
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.LibraryRepository.getLibrary(khachhang.Id);
                    ViewBag.DonHang = proLib;
                }
            }
        }

        [Route("cart.html", Name = "Cart")]
        public ActionResult Cart()
        {
            GetLibrary();
            return View(GioHang);
        }
    }
}
