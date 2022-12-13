using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
using Khoaluan.Enums;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Dev")]
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
                        _notyfService.Warning("Please log out at User");
                        return RedirectToAction("Dashboard", "Users");
                    }
                    var kh = _unitOfWork.DeveloperRepository.getDev(model.UserName);

                    if (kh == null)
                    {
                        ViewBag.Eror = "Login information is incorrect";
                        return View(model);
                    }
                    string pass = (model.Password.Trim());
                    // + kh.Salt.Trim()
                    if (kh.Passwork.Trim() != pass)
                    {
                        ViewBag.Eror = "Login information is incorrect";
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
        public IActionResult Create()
        {
            //ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name");
            return View();
        }
        public IActionResult IndexDev()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.Status == (int)productType.accept && t.DevId==int.Parse(taikhoanID)).ToList();
            return View(ls);
        }
        // POST: AdminProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Overview,Description,Price,Image,DevId,ReleaseDate,Status")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int type = (int)productType.pending;
                    product.Name = Utilities.ToTitleCase(product.Name);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.Name) + extension;
                        product.Image = await Utilities.UploadFile(fThumb, image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Image)) product.Image = "default.jpg";
                    var taikhoanID = HttpContext.Session.GetString("AccountId");
                    product.Status = type;
                    product.DevId = int.Parse(taikhoanID);
                    _unitOfWork.ProductRepository.Create(product);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Successfully added new");
                    return RedirectToAction(nameof(IndexDev));
                }
                catch (Exception)
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(IndexDev));
                }
            }
            //ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }

        // GET: DevController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.ProductRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            List<string> cate = new List<string>();
            var product1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
            if (product1 != null)
            {
                cate.AddRange(product1.Categories);
                ViewData["Category"] = cate;
            }
            else
            {
                ViewData["Category"] = "";
            }

            var data = new List<SelectListItem>();
            foreach (var item in _unitOfWork.CategoryRepository.GetAll())
            {
                data.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            foreach (var item_1 in cate)
            {
                foreach (var item_2 in data)
                {
                    if (item_2.Text.Equals(item_1))
                    {
                        item_2.Selected = true;
                    }
                }
            }

            MultiDropDownListViewModel model = new();
            model.ItemList = data;

            ProCate pwc = new ProCate()
            {
                product = product,
                muti = model
            };

            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(pwc);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Overview,Description,Price,Image,DevId,ReleaseDate,Status")] Product product, PostSelectedViewModel model, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                try
                {
                    if (model.SelectedIds == null)
                    {
                        _notyfService.Warning("Please select a category");
                        return RedirectToAction(nameof(IndexDev));
                    }
                    product.Name = Utilities.ToTitleCase(product.Name);
                    if (fThumb != null)
                    {


                        string extension = Path.GetExtension(fThumb.FileName);
                        string images = Utilities.SEOUrl(product.Name) + extension;
                        product.Image = await Utilities.UploadFile(fThumb, images.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Image)) product.Image = "default.jpg";
                    var catepro = _unitOfWork.ProductCategoryRepository.GetAll().Where(t => t.ProductId == id);
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.ProductCategoryRepository.BulkDelete(catepro.ToList());
                    _unitOfWork.ProductCategoryRepository.updateCategory(id, model);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Update successful");
                    List<string> cate = new List<string>();
                    var product1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
                    if (product1 != null)
                    {
                        cate.AddRange(product1.Categories);
                        ViewData["Category"] = cate;
                    }
                    else
                    {
                        ViewData["Category"] = "";
                    }
                    return RedirectToAction(nameof(IndexDev));
                }
                catch (Exception)
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(IndexDev));
                }
            }
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
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
                        _notyfService.Success("Change password successfully");
                        return RedirectToAction("InfoDev", "Dev", new { Area = "Admin" });
                    }
                }
            }
            catch
            {
                _notyfService.Success("Password change failed");
                return RedirectToAction("Info", "Admin", new { Area = "Admin" });
            }
            _notyfService.Success("Password change failed");
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
