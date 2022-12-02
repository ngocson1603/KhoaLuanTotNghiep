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

namespace Khoaluan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }

        public string Role
        {
            get
            {
                var gh = HttpContext.Session.GetString("Role");
                if (gh == null)
                {
                    gh = "";
                }
                return gh;
            }
        }
        [Route("tai-khoan.html", Name = "Info")]
        public IActionResult Info()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan ==taikhoanID);
                if (khachhang != null)
                {
                    return View(khachhang);
                }
            }
            return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
        }
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public IActionResult AdminLogin(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("login.html", Name = "Login")]
        public async Task<IActionResult> AdminLogin(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("User"))
                    {
                        await HttpContext.SignOutAsync();
                        HttpContext.Session.Remove("CustomerId");
                        //_notyfService.Warning("Vui lòng đăng xuất ở User");
                        //return RedirectToAction("Dashboard", "Users");
                    }
                    var kh = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan.Trim() == model.Gmail);

                    if (kh == null)
                    {
                        ViewBag.Eror = "Thông tin đăng nhập chưa chính xác";
                    }
                    string pass = (model.Password.Trim());
                    // + kh.Salt.Trim()
                    if (kh.Password.Trim() != pass)
                    {
                        ViewBag.Eror = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
                    }
                    //đăng nhập thành công

                    //ghi nhận thời gian đăng nhập
                    kh.LastLogin = DateTime.Now;
                    _unitOfWork.AdminRepository.Update(kh);
                    _unitOfWork.SaveChange();


                    var taikhoanID = HttpContext.Session.GetString("AccountId");
                    //identity
                    //luuw seccion Makh
                    HttpContext.Session.SetString("AccountId", kh.TaiKhoan.ToString());
                    HttpContext.Session.SetString("Role", "Admin");
                    var Roles = HttpContext.Session.GetString("Role");
                    //identity
                    var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, kh.HoTen),
                            new Claim(ClaimTypes.Email, kh.TaiKhoan),
                            new Claim("AccountId", kh.TaiKhoan.ToString()),
                            new Claim(ClaimTypes.Role, Roles)
                        };
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    await HttpContext.SignInAsync(userPrincipal);



                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
            }
            return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("AccountId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _unitOfWork.AdminRepository.GetById(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Users");
                    var pass = (model.PasswordNow.Trim());
                    {
                        string passnew = (model.Password.Trim());
                        taikhoan.Password = passnew;
                        _unitOfWork.AdminRepository.Update(taikhoan);
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("Info", "Admin", new { Area = "Admin" });
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
        [Route("logout.html", Name = "Logout")]
        public IActionResult AdminLogout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                HttpContext.Session.Remove("Role");
                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
            }
            catch
            {
                return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
            }
        }
    }
}
