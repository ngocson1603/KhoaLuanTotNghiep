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
    [Authorize]
    public class AdminProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminProductsController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        // GET: AdminProductsController
        public IActionResult Index()
        {
            var ls = _unitOfWork.ProductRepository.GetAll().ToList();

            return View(ls);
        }

        // GET: AdminProductsController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ls = _unitOfWork.ProductRepository.GetById((int)id);
            if (ls == null)
            {
                return NotFound();
            }

            return View(ls);
        }

        // GET: AdminProductsController/Create
        public IActionResult Create()
        {
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: AdminProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Overview,Description,Price,Image,DevId,ReleaseDate")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                product.Name = Utilities.ToTitleCase(product.Name);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(product.Name) + extension;
                    product.Image = await Utilities.UploadFile(fThumb, image.ToLower());
                }
                if (string.IsNullOrEmpty(product.Image)) product.Image = "default.jpg";
                product.ReleaseDate = DateTime.Now;

                _unitOfWork.ProductRepository.Create(product);
                _unitOfWork.SaveChange();
                _notyfService.Success("Thêm mới thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }

        // GET: AdminProductsController/Edit/5
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
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Overview,Description,Price,Image,DevId,ReleaseDate,Status")] Product product, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                product.Name = Utilities.ToTitleCase(product.Name);
                if (fThumb != null)
                {


                    string extension = Path.GetExtension(fThumb.FileName);
                    string images = Utilities.SEOUrl(product.Name) + extension;
                    product.Image = await Utilities.UploadFile(fThumb, images.ToLower());
                }
                if (string.IsNullOrEmpty(product.Image)) product.Image = "default.jpg";
                product.ReleaseDate = DateTime.Now;

                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.SaveChange();
                _notyfService.Success("Cập nhật thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }

        // GET: AdminProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChange();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        // POST: AdminProductsController/Delete/5
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
