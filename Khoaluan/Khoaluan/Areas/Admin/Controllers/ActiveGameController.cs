using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
using Khoaluan.Enums;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan.OtpModels;
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
    public class ActiveGameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public ActiveGameController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t=>t.Status == (int)productType.pending).ToList();

            return View(ls);
        }

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
                        return RedirectToAction(nameof(Index));
                    }
                    if (product.Status == 0)
                    {
                        _notyfService.Warning("Please select a status");
                        return RedirectToAction(nameof(Index));
                    }
                    product.Name = Utilities.ToTitleCase(product.Name);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string images = Utilities.SEOUrl(product.Name) + extension;
                        product.Image = await Utilities.UploadFile(fThumb, images.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Image)) product.Image = "default.jpg";
                    product.ReleaseDate = DateTime.Now;
                    var catepro = _unitOfWork.ProductCategoryRepository.GetAll().Where(t => t.ProductId == id);
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.ProductCategoryRepository.BulkDelete(catepro.ToList());
                    _unitOfWork.ProductCategoryRepository.updateCategory(id, model);
                    _unitOfWork.SaveChange();
                    var pro = _unitOfWork.ProductRepository.listProdevActive(id);
                    Utilities.sendemailactivegame(pro);
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }
    }
}
