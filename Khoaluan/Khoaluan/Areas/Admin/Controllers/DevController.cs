using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
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
    [Area("Admin")]
    [Authorize]
    public class DevController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public DevController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }

        [Route("tai-khoan-dev.html", Name = "InfoDev")]
        public IActionResult InfoDev()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.DeveloperRepository.GetById(int.Parse(taikhoanID));
                if (khachhang != null)
                {
                    return View(khachhang);
                }
            }
            return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
        }

        [AllowAnonymous]
        [Route("logindev.html", Name = "Logindev")]
        public IActionResult LoginDev(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("logindev.html", Name = "Logindev")]
        public async Task<IActionResult> LoginDev(LoginDevViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("User"))
                    {
                        _notyfService.Warning("Vui lòng đăng xuất ở User");
                        return RedirectToAction("Dashboard", "Users");
                    }
                    var kh = _unitOfWork.DeveloperRepository.getDev(model.UserName);

                    if (kh == null)
                    {
                        ViewBag.Eror = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    string pass = (model.Password.Trim());
                    // + kh.Salt.Trim()
                    if (kh.Passwork.Trim() != pass)
                    {
                        ViewBag.Eror = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    //đăng nhập thành công


                    var taikhoanID = HttpContext.Session.GetString("AccountId");
                    //identity
                    //luuw seccion Makh
                    HttpContext.Session.SetString("AccountId", kh.Id.ToString());
                    HttpContext.Session.SetString("Role", "Dev");
                    var Roles = HttpContext.Session.GetString("Role");
                    //identity
                    var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, kh.Name),
                            new Claim(ClaimTypes.Email, kh.UserName),
                            new Claim("AccountId", kh.Id.ToString()),
                            new Claim(ClaimTypes.Role, Roles)
                        };
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "Dev Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    await HttpContext.SignInAsync(userPrincipal);



                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
            }
            return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
        }

        // GET: DevController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DevController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DevController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DevController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DevController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DevController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DevController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("AccountId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _unitOfWork.DeveloperRepository.GetById(int.Parse(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
                    var pass = (model.PasswordNow.Trim());
                    {
                        string passnew = (model.Password.Trim());
                        taikhoan.Passwork = passnew;
                        _unitOfWork.DeveloperRepository.Update(taikhoan);
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("InfoDev", "Dev", new { Area = "Admin" });
                    }
                }
            }
            catch
            {
                _notyfService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Info", "Admin", new { Area = "Admin" });
            }
            _notyfService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Info", "Admin", new { Area = "Admin" });
        }
        public IActionResult LogoutDev()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                HttpContext.Session.Remove("Role");
                return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
            }
            catch
            {
                return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
            }
        }
    }
}
