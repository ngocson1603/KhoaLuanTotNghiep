using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Extension;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public UsersController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ValidatePhone(string Phone)
        //{
        //    try
        //    {
        //        var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
        //        if (khachhang != null)
        //            return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

        //        return Json(data: true);

        //    }
        //    catch
        //    {
        //        return Json(data: true);
        //    }
        //}
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ValidateEmail(string Email)
        //{
        //    try
        //    {
        //        var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
        //        if (khachhang != null)
        //            return Json(data: "Email : " + Email + " đã được sử dụng");
        //        return Json(data: true);
        //    }
        //    catch
        //    {
        //        return Json(data: true);
        //    }
        //}
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    //var lsDonHang = _context.Orders
                    //    .Include(x => x.TransactStatus)
                    //    .AsNoTracking()
                    //    .Where(x => x.CustomerId == khachhang.CustomerId)
                    //    .OrderByDescending(x => x.OrderDate)
                    //    .ToList();
                    //ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangkyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Users khachhang = new Users
                    {
                        HoTen = taikhoan.FullName,
                        Gmail = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Salt = salt,
                    };
                    try
                    {
                        _unitOfWork.UserRepository.Create(khachhang);
                        _unitOfWork.SaveChange();
                        //Lưu Session MaKh
                        HttpContext.Session.SetString("CustomerId", khachhang.Id.ToString());
                        var taikhoanID = HttpContext.Session.GetString("CustomerId");

                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.HoTen),
                            new Claim("CustomerId", khachhang.Id.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đăng ký thành công");
                        return RedirectToAction("Dashboard", "Users");
                    }
                    catch
                    {
                        return RedirectToAction("DangkyTaiKhoan", "Users");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }

        [Authorize, HttpPost]
        public IActionResult Refund(int productID)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                var refundID = _unitOfWork.RefundRepository.refundID(int.Parse(taikhoanID), productID);
                try
                {
                    _unitOfWork.LibraryRepository.remove(int.Parse(taikhoanID), productID);
                    _unitOfWork.RefundRepository.refund(int.Parse(taikhoanID), productID, refundID);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("thành công");
                    return RedirectToRoute("Library");
                }
                catch (Exception ex)
                {
                    //log
                    return RedirectToRoute("Library");
                }

            }
            return RedirectToRoute("Library");
        }

        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "Users");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Utilities.IsValidEmail(customer.Gmail);
                    if (!isEmail) return View(customer);

                    var khachhang = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Gmail.Trim() == customer.Gmail);
                    if (khachhang == null) return RedirectToAction("DangkyTaiKhoan");
                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        _notyfService.Success("Thông tin đăng nhập chưa chính xác");
                        return View(customer);
                    }
                    //kiem tra xem account co bi disable hay khong

                    //if (khachhang.Active == false)
                    //{
                    //    return RedirectToAction("ThongBao", "Accounts");
                    //}

                    //Luu Session MaKh
                    HttpContext.Session.SetString("CustomerId", khachhang.Id.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");

                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.HoTen),
                        new Claim("CustomerId", khachhang.Id.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Dashboard", "Users");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            catch
            {
                return RedirectToAction("DangkyTaiKhoan", "Users");
            }
            return View(customer);
        }
        [HttpGet]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("HomePage", "Product");
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _unitOfWork.UserRepository.GetById(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Users");
                    var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    {
                        string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.Password = passnew;
                        _unitOfWork.UserRepository.Update(taikhoan);
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("Dashboard", "Users");
                    }
                }
            }
            catch
            {
                _notyfService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Users");
            }
            _notyfService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Users");
        }
        [Route("library.html", Name = "Library")]
        public IActionResult Library()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.UserRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.LibraryRepository.getLibrary(khachhang.Id);
                    ViewBag.DonHang = proLib;
                    return View(khachhang);
                }
            }
            return RedirectToAction("Login");
        }
    }
}
