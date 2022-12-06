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
        private readonly IService _service;

        public ItemController(IUnitOfWork unitOfWork, INotyfService notyfService, IService service)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _service = service;
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
        public ActionResult SellItem(int Id, int number, int price)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");                
                var inventory = _unitOfWork.InventoryRepository.GetAll().Where(t => (t.UserID == int.Parse(taikhoanID)) && (t.ItemID == Id)).Single();
                var user = _unitOfWork.UserRepository.GetById(int.Parse(taikhoanID));
                try
                {
                    if (number > inventory.Quantity)
                    {
                        _notyfService.Warning("số lượng không đủ");
                        return RedirectToAction(nameof(ListItem));
                    }

                    Market mk = new Market()
                    {
                        UserID = int.Parse(taikhoanID),
                        ItemID = Id,
                        Price = price,
                        DayCreate = DateTime.Now,
                        Status = (int)marketType.sell,
                        PricePerItem = price,
                        Quantity = number
                    };
                    _unitOfWork.MarketRepository.Create(mk);
                    _service.InventoryService.updateInventory(int.Parse(taikhoanID), Id, (int)marketType.sell, number);                   
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

        [HttpPost]
        [Route("api/item/buy")]
        public ActionResult BuyItem(int Id, int number, int price)
        {
            if (ModelState.IsValid)
            {
                var market = _unitOfWork.MarketRepository.GetById(Id);
                var usersell = market.UserID;
                var taikhoanID = HttpContext.Session.GetString("CustomerId");               
                var user = _unitOfWork.UserRepository.GetById(int.Parse(taikhoanID));
                try
                {
                    if (number > market.Quantity)
                    {
                        _notyfService.Warning("số lượng nhiều hơn số lượng đã có");
                        return RedirectToAction(nameof(ListItem));
                    }
                    if (price < user.Balance)
                    {
                        var total = number * price;
                        TransactionRequest request = new TransactionRequest()
                        {
                            MarketID = Id,
                            buyerID = user.Id,
                            sellerID = usersell,
                            itemID = market.ItemID,
                            quantity = number,
                            totalprice = total
                        };
                        market.Quantity = market.Quantity - number;
                        var transaction = _service.MarketTransactionService.Transaction(request);
                        var buyer = _service.UserService.updateBalance(user.Id, total, (int)marketType.buy);
                        var seller = _service.UserService.updateBalance(usersell, total, (int)marketType.sell);
                        var invenbuyer = _service.InventoryService.updateInventory(user.Id, market.ItemID, (int)marketType.buy, number);
                        var invenseller = _service.InventoryService.updateInventory(usersell, market.ItemID, (int)marketType.sell, number);
                        if (market.Quantity == 0)
                        {
                            market.Status = (int)marketType.soldout;
                        }
                        if (invenbuyer == null)
                        {
                            Inventory inven = new Inventory()
                            {
                                UserID = user.Id,
                                ItemID = market.ItemID,
                                Quantity = number,
                            };
                            _unitOfWork.InventoryRepository.Create(inven);
                        }
                        else
                        {
                            _unitOfWork.InventoryRepository.Update(invenbuyer);
                        }
                        _unitOfWork.MarketTransactionRepository.Create(transaction);
                        _unitOfWork.InventoryRepository.Update(invenseller);
                        _unitOfWork.UserRepository.Update(buyer);
                        _unitOfWork.UserRepository.Update(seller);
                        _unitOfWork.SaveChange();
                    }
                    else
                    {
                        _notyfService.Warning("Vui lòng nhạp thêm tiền");
                        return RedirectToAction("Dashboard", "Users");
                    }
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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.ItemRepository.getItemById((int)id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

    }
}
