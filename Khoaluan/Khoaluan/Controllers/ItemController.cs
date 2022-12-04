using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
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
            ViewBag.ListItem = item;
            return View(ls);
        }
        [HttpPost]
        [Route("sell/item")]
        public IActionResult SellItem(int itemId)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                int type = (int)marketType.sell;
                try
                {
                    _inventoryService.updateInventory(int.Parse(taikhoanID), itemId, type,1);
                    return Redirect("/ProductCart/CheckoutSuccess");
                }
                catch (Exception ex)
                {
                    //log
                    return Redirect("/ProductCart/CheckoutFail");
                }

            }
            return RedirectToRoute("Cart");
        }
    }
}
