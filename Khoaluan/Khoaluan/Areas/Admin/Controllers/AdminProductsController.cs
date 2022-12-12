using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Areas.Admin.Models;
using Khoaluan.Enums;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan.OtpModels;
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
    public class AdminProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminProductsController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        public int ProductID
        {
            get
            {
                int? gh = int.Parse(HttpContext.Session.GetString("ProductID"));
                if (gh == null)
                {
                    gh = 0;
                }
                return (int)gh;
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: AdminProductsController
        public IActionResult Index()
        {
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t => t.Status == (int)productType.accept || t.Status == (int)productType.release).ToList();

            return View(ls);
        }

        // GET: AdminProductsController/Details/5
        [Authorize(Roles = "Admin,Dev")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ls = _unitOfWork.ProductRepository.GetById((int)id);
            var item = _unitOfWork.ItemRepository.GetAll().Where(t=>t.ProductId == id).ToList();
            if (ls == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("ProductID", id.ToString());
            AdminProduct pwc = new AdminProduct()
            {
                product = ls,
                item = item
            };
            List<string> cate = new List<string>();
            var product1 = _unitOfWork.ProductRepository.getallProductwithCategory().Where(t => t.Id == id).FirstOrDefault();
            if(product1 != null)
            {
                cate.AddRange(product1.Categories);
                ViewData["Category"] = cate;
            }
            else
            {
                ViewData["Category"] = "";
            }
            
            return View(pwc);
        }
        public IActionResult CreateGame()
        {
            var data = new List<MultiDropDownListViewModel>();
            foreach (var item in _unitOfWork.CategoryRepository.GetAll())
            {
                data.Add(new MultiDropDownListViewModel { Id = item.Id, Name = item.Name });
            }
            MultiDropDownListViewModel model = new();
            model.ItemList = data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewData["Cate"] = model;
            return View(model);
        }

        [HttpPost]
        public IActionResult PostSelectedValues(PostSelectedViewModel model)
        {
            List<int> lst = model.SelectedIds.ToList();
            lst.Remove(2);
            return View();
        }

        // GET: AdminProductsController/Create




        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                //var searchProCate = _unitOfWork.ProductCategoryRepository.GetAll().Where(t => t.ProductId == id);
                //List<int> lst = model.SelectedIds.ToList();
                //List<int> lst2 = new List<int>();
                //foreach(var item in searchProCate.Select(t => t.CategoryId))
                //{
                //    lst2.Add(item);
                //}
                //var result = lst.Except(lst2).ToList();
                if (model.SelectedIds == null)
                {
                    _notyfService.Warning("Please select a category");
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
            ViewData["Developer"] = new SelectList(_unitOfWork.DeveloperRepository.GetAll(), "Id", "Name", product.DevId);
            return View(product);
        }

        // GET: AdminProductsController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChange();
            _notyfService.Success("Delete successful");
            return RedirectToAction(nameof(Index));
        }

        // POST: AdminProductsController/Delete/5
        [Authorize(Roles = "Admin")]
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
