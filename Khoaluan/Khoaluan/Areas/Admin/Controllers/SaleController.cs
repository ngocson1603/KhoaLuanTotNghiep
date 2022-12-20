using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public SaleController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        // GET: SaleController

        [Route("/sale.html", Name = "Index")]
        public ActionResult Index()
        {
            var sale = _unitOfWork.SaleRepository.GetAll();
            return View(sale);
        }
        // GET: SaleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Edits(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleid = HttpContext.Session.GetInt32("SaleId");
            var product = _unitOfWork.SaleProductRepository.ProductIsSale((int)saleid).Where(t=>t.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: AdminProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edits(int id, SaleModelView saleModelView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaleProduct sale = new SaleProduct()
                    {
                        Id = id,
                        SaleID = saleModelView.SaleId,
                        ProductID = saleModelView.ProductId,
                        Discount = saleModelView.Discount
                    };
                    _unitOfWork.SaleProductRepository.Update(sale);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Update successful");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(saleModelView);
        }

        // POST: SaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,StartDate,EndDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(sale.StartDate > sale.EndDate)
                    {
                        _notyfService.Warning("Start date incorrect");
                        return RedirectToAction(nameof(Index));
                    }
                    _unitOfWork.SaleRepository.Create(sale);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Successfully added new");
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }
            }
            _notyfService.Success("Error");
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DeleteSeleProduct(int id)
        {
            try
            {
                var product = _unitOfWork.SaleProductRepository.GetById(id);
                _unitOfWork.SaleProductRepository.Delete(product);
                _unitOfWork.SaveChange();
                _notyfService.Success("Delete successful");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: SaleController/Edit/5
        public ActionResult Edit(int id)
        {
            var ls = _unitOfWork.ProductRepository.GetAll().Where(t=>t.Price > 0).ToList();
            ViewBag.SaleId = id.ToString();
            return View(ls);
        }

        public ActionResult ViewProductSale(int id)
        {
            try
            {
                HttpContext.Session.SetInt32("SaleId", id);
                var ls = _unitOfWork.SaleProductRepository.ProductIsSale(id).ToList();
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
            
        }

        public ActionResult ViewProductSaleInDate()
        {
            try
            {
                var ls = _unitOfWork.SaleProductRepository.ProductIsSaleInDate().ToList();
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [Route("/Sale/AjaxMethod", Name = "AjaxMethod")]
        public JsonResult AjaxMethod(string saleId, string productId, string discount)
        {
            if (_unitOfWork.SaleRepository.GetById(int.Parse(saleId)) == null)
                return null;
            try
            {
                _unitOfWork.SaleProductRepository.Create(new SaleProduct()
                {
                    SaleID = int.Parse(saleId),
                    ProductID = int.Parse(productId),
                    Discount = int.Parse(discount)
                });
                _unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                throw;
            }

            return Json(1);
        }

        [HttpPost]
        public void SaveSaleProduct(string saleId, string productId, string discount)
        {
            
        }

        // POST: SaleController/Edit/5
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

        // GET: SaleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SaleController/Delete/5
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
