using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public IInventoryService _inventoryService;
        public ItemController(IUnitOfWork unitOfWork, INotyfService notyfService, IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _inventoryService = inventoryService;
        }
        [Route("ListItem.html", Name = "ListItem")]
        public IActionResult ListItem()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            var ls = _unitOfWork.ItemRepository.GetAll();
            var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID));
            AdminProduct ad = new AdminProduct()
            {
                item = ls,
                itembyID = item
            };
            return View(ad);
        }
        [HttpPost]
        [Route("api/item/sell")]
        public ActionResult SellItem(int Id)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                int type = (int)marketType.sell;
                try
                {
                    _inventoryService.updateInventory(int.Parse(taikhoanID), Id, type,1);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("thành công");
                    return RedirectToRoute("ListItem");
                }
                catch (Exception ex)
                {
                    _notyfService.Success("khong thành công");
                    return RedirectToRoute("ListItem");
                }

            }
            _notyfService.Success("khong thành công");
            return RedirectToRoute("ListItem");
        }
    }
}
