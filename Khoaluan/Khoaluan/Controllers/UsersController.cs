﻿using AspNetCoreHero.ToastNotification.Abstractions;
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
        public INotyfService _notyfService { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;

        public UsersController(INotyfService notyfService, IUnitOfWork unitOfWork, IService service)
        {
            _notyfService = notyfService;
            _unitOfWork = unitOfWork;
            _service = service;
        }

        /* ValidatePhone
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        */

        /* ValidateEmail
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        */

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
                    ViewBag.NumberOfGames = _unitOfWork.LibraryRepository.getLibrary(khachhang.Id).Count();
                    return View(khachhang);
                }
            }

            return RedirectToAction("Homepage", "Product");
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
        public async Task<IActionResult> DangkyTaiKhoanAsync(RegisterViewModel taikhoan)
        {
            if (taikhoan.ConfirmPassword != taikhoan.Password)
            {
                _notyfService.Warning("The password confirmation does not match");
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                _notyfService.Warning("Please check your information and try again");
                return RedirectToAction("Index", "Home");
            }

            int kq = _service.UserService.SignUp(taikhoan);

            if (kq == -1)
            {
                _notyfService.Warning("This email is already in use");
            }
            else if (kq == 0)
            {
                _notyfService.Error("Registration failed");
            }
            else
            {
                _unitOfWork.SaveChange();

                // Lưu Session KH
                var user = _unitOfWork.UserRepository.FindByEmail(taikhoan.Email);
                HttpContext.Session.SetString("CustomerId", user.Id.ToString());
                HttpContext.Session.SetString("Role", "User");

                // Identity
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.HoTen),
                            new Claim("CustomerId", user.Id.ToString()),
                            new Claim(ClaimTypes.Role, "User")
                        };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                _notyfService.Success("Registration successful");

                return RedirectToAction("Dashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize, HttpPost]
        public IActionResult Refund(int productID)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                int userid = int.Parse(taikhoanID);
                try
                {
                    Models.Refund refundrequest = _service.RefundService.refund(userid, productID);
                    _unitOfWork.RefundRepository.Create(refundrequest);
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
        public async Task<IActionResult> Login(LoginViewModel customer)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Dev"))
            {
                _notyfService.Warning("Please logout your user account first");
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            if (!ModelState.IsValid)
            {
                _notyfService.Warning("Please check your information and try again");
                return RedirectToAction("Index", "Home");
            }

            int kq = _service.UserService.SignIn(customer);

            if (kq == -1)
            {
                _notyfService.Warning("Couldn't find your account");
            }
            else if (kq == 2)
            {
                _notyfService.Error("Incorrect email or password");
            }
            else
            {
                // Lưu Session KH
                var user = _unitOfWork.UserRepository.FindByEmail(customer.Gmail);
                HttpContext.Session.SetString("CustomerId", user.Id.ToString());
                HttpContext.Session.SetString("Role", "User");

                // Identity
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.HoTen),
                        new Claim("CustomerId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User")
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                _notyfService.Success($"Welcome back, {user.HoTen}!");

                return RedirectToAction("Dashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Homepage", "Product");
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
