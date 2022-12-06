using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Dev")]
    public class DevItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public DevItemController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        // GET: DevItemController
        public ActionResult Index()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            var ls = _unitOfWork.ItemRepository.getItem(int.Parse(taikhoanID));

            return View(ls);
        }
        public IActionResult IndexDev()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            var ls = _unitOfWork.ProductRepository.listProductDev(int.Parse(taikhoanID)).ToList();
            return View(ls);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem([Bind("Id,Name,Image,ProductId,MinPrice,MaxPrice")] Item item, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                item.Name = Utilities.ToTitleCase(item.Name);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(item.Name) + extension;
                    item.Image = await Utilities.UploadFile(fThumb, image.ToLower());
                }
                if (string.IsNullOrEmpty(item.Image)) item.Image = "default.jpg";
                var idproduct = HttpContext.Session.GetString("ProductID");
                item.ProductId = int.Parse(idproduct);
                _unitOfWork.ItemRepository.Create(item);
                _unitOfWork.SaveChange();
                _notyfService.Success("Thêm mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        // GET: DevItemController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<string> cate = new List<string>();
            var item = _unitOfWork.ItemRepository.getItemById((int)id);
            var product1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            cate.AddRange(product1.Categories);
            ViewData["Category"] = cate;
            return View(item);
        }

        // GET: DevItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DevItemController/Create
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

        // GET: DevItemController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _unitOfWork.ItemRepository.GetById((int)id);
            if (item == null)
            {
                return NotFound();
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            ViewData["Item"] = new SelectList(_unitOfWork.ProductRepository.listProductDev(int.Parse(taikhoanID)), "Id", "Name", item.ProductId);
            return View(item);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,ProductId,MaxPrice,MinPrice")] Item item, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                item.Name = Utilities.ToTitleCase(item.Name);
                if (fThumb != null)
                {


                    string extension = Path.GetExtension(fThumb.FileName);
                    string images = Utilities.SEOUrl(item.Name) + extension;
                    item.Image = await Utilities.UploadFile(fThumb, images.ToLower());
                }
                if (string.IsNullOrEmpty(item.Image)) item.Image = "default.jpg";

                _unitOfWork.ItemRepository.Update(item);
                _unitOfWork.SaveChange();
                _notyfService.Success("Cập nhật thành công");
                return RedirectToAction(nameof(Index));
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            ViewData["Item"] = new SelectList(_unitOfWork.ProductRepository.listProductDev(int.Parse(taikhoanID)), "Id", "Name", item.ProductId);
            return View(item);
        }
        // GET: DevItemController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.ItemRepository.GetById(id);
            _unitOfWork.ItemRepository.Delete(product);
            _unitOfWork.SaveChange();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        // POST: DevItemController/Delete/5
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
    }
}
