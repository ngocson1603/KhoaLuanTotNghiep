using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.DeveloperRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Passwork,UserName")] Developer developer)
        {
            if (id != developer.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.DeveloperRepository.Update(developer);
                _unitOfWork.SaveChange();
                _notyfService.Success("Cập nhật thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.DeveloperRepository.GetById(id);
            _unitOfWork.DeveloperRepository.Delete(product);
            _unitOfWork.SaveChange();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Index()
        {
            var ls = _unitOfWork.DeveloperRepository.GetAll().ToList();
            return View(ls);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dev = _unitOfWork.DeveloperRepository.GetAll().Where(t => t.Id == id).FirstOrDefault();
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.DevId == id).ToList();
            var item = _unitOfWork.ItemRepository.GetAll().Where(t => t.Id == id).ToList();
            if (ls == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("ProductID", id.ToString());
            AdminProduct pwc = new AdminProduct()
            {
                dev = dev,
                productdev = ls,
                item = item
            };
            return View(pwc);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Passwork,UserName")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DeveloperRepository.Create(developer);
                _unitOfWork.SaveChange();
                _notyfService.Success("Thêm mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
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
                var khachhang = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan == taikhoanID);
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
                        //await HttpContext.SignOutAsync();
                        //HttpContext.Session.Remove("CustomerId");
                        _notyfService.Warning("Vui lòng đăng xuất ở User");
                        return RedirectToAction("Dashboard", "Users");
                    }
                    var kh = _unitOfWork.AdminRepository.GetAll().SingleOrDefault(x => x.TaiKhoan.Trim() == model.Gmail);

                    if (kh == null)
                    {
                        ViewBag.Eror = "Thông tin đăng nhập chưa chính xác";
                        return View(model);
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
                    var taikhoan = _unitOfWork.AdminRepository.GetAll().Where(t => t.TaiKhoan == taikhoanID).FirstOrDefault();
                    if (taikhoan == null) return RedirectToAction("AdminLogin", "Admin", new { Area = "Admin" });
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
