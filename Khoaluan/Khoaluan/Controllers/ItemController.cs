using AspNetCoreHero.ToastNotification.Abstractions;
using Khoaluan.Enums;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Controllers
{
    [Authorize]
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
        public IActionResult ListItem(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            var ls = _unitOfWork.ItemRepository.getItemSell();
            var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID));
            var product = _unitOfWork.ProductRepository.listProductItem().OrderBy(i => i.Id).ToList();
            if (ls.Count() <= 10)
                ViewBag.maxPage = 1;
            else
            {
                double dMaxPage = Convert.ToDouble(ls.Count());
                ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
            }
            var pl = ls.AsQueryable().ToPagedList(pageNumber, pageSize);
            var plr = pl.ToList();
            AdminProduct ad = new AdminProduct()
            {
                itembySell = plr,
                itembyID = item,
                productdev = product
            };
            ViewBag.CurrentPage = pageNumber;
            return View(ad);
        }

        [Route("/ListItem/{id}.html", Name = ("ListItemProduct"))]
        public IActionResult ItemProduct(string id, int? page)
        {
            try
            {
                ViewBag.idproitem = id.Trim();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                var ls = _unitOfWork.ItemRepository.getItemSell().Where(t=>t.NameGame.Equals(id)).ToList();
                var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID));
                var product = _unitOfWork.ProductRepository.listProductItem().OrderBy(i => i.Id).ToList();
                if (ls.Count() <= 10)
                    ViewBag.maxPage = 1;
                else
                {
                    double dMaxPage = Convert.ToDouble(ls.Count());
                    ViewBag.maxPage = Math.Ceiling(dMaxPage / 10);
                }

                var pl = ls.AsQueryable().ToPagedList(pageNumber, pageSize);
                var plr = pl.ToList();
                AdminProduct ad = new AdminProduct()
                {
                    itembySell = plr,
                    itembyID = item,
                    productdev = product
                };
                ViewBag.CurrentPage = pageNumber;
                return View(ad);
            }
            catch
            {
                return RedirectToRoute("ListItem");
            }
        }

        public ActionResult DetailsItemSell(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            var item = _unitOfWork.ItemRepository.getItemByUser(int.Parse(taikhoanID));

            if (item == null)
            {
                return NotFound();
            }
            return PartialView("ListItemUserPartialView", item);
        }
        [HttpPost]
        [Route("api/item/getid")]
        public void GetID(int id)
        {
            HttpContext.Session.SetInt32("SetIdItem",id);
        }

        [HttpPost]
        [Route("api/item/sell")]
        public ActionResult SellItem(SellitemModelView sellitem)
        {
            if (ModelState.IsValid)
            {
                int Id = (int)HttpContext.Session.GetInt32("SetIdItem");
                var taikhoanID = HttpContext.Session.GetString("CustomerId");                
                var inventory = _unitOfWork.InventoryRepository.GetAll().Where(t => (t.UserID == int.Parse(taikhoanID)) && (t.ItemID == Id)).Single();
                var user = _unitOfWork.UserRepository.GetById(int.Parse(taikhoanID));
                try
                {
                    if (sellitem.Quantity > inventory.Quantity)
                    {
                        _notyfService.Warning("quantity is not enough");
                        return RedirectToAction(nameof(ListItem));
                    }

                    Market mk = new Market()
                    {
                        UserID = int.Parse(taikhoanID),
                        ItemID = Id,
                        Price = sellitem.PricePerItem,
                        DayCreate = DateTime.Now,
                        Status = (int)marketType.sell,
                        PricePerItem = sellitem.PricePerItem,
                        Quantity = sellitem.Quantity
                    };
                    _unitOfWork.MarketRepository.Create(mk);
                    _service.InventoryService.updateInventory(int.Parse(taikhoanID), Id, (int)marketType.sell, sellitem.Quantity);                   
                    _unitOfWork.SaveChange();
                    _notyfService.Success("successful");
                    return RedirectToRoute("ListItem");
                }
                catch (Exception ex)
                {
                    _notyfService.Success("unsuccessful");
                    return RedirectToRoute("ListItem");
                }
            }
            _notyfService.Success("unsuccessful");
            return RedirectToRoute("ListItem");
        }

        [HttpPost]
        public ActionResult BuyItem(int Id, SellitemModelView sellitem)
        {
            if (ModelState.IsValid)
            {
                var market = _unitOfWork.MarketRepository.GetById(Id);
                var usersell = market.UserID;
                var taikhoanID = HttpContext.Session.GetString("CustomerId");               
                var user = _unitOfWork.UserRepository.GetById(int.Parse(taikhoanID));
                try
                {
                    if (sellitem.Count > market.Quantity)
                    {
                        _notyfService.Warning("quantity is more than the amount already available");
                        return RedirectToAction(nameof(ListItem));
                    }
                    if (sellitem.PricePerItem < user.Balance)
                    {
                        var total = sellitem.Count * sellitem.PricePerItem;
                        TransactionRequest request = new TransactionRequest()
                        {
                            MarketID = Id,
                            buyerID = user.Id,
                            sellerID = usersell,
                            itemID = market.ItemID,
                            quantity = sellitem.Count,
                            totalprice = total
                        };
                        market.Quantity = market.Quantity - sellitem.Count;
                        var transaction = _service.MarketTransactionService.Transaction(request);
                        var buyer = _service.UserService.updateBalance(user.Id, total, (int)marketType.buy);
                        var seller = _service.UserService.updateBalance(usersell, total, (int)marketType.sell);
                        var invenbuyer = _service.InventoryService.updateInventory(user.Id, market.ItemID, (int)marketType.buy, sellitem.Count);
                        var invenseller = _service.InventoryService.updateInventory(usersell, market.ItemID, (int)marketType.sell, sellitem.Count);
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
                                Quantity = sellitem.Count,
                            };
                            _unitOfWork.InventoryRepository.Create(inven);
                        }
                        else
                        {
                            _unitOfWork.InventoryRepository.Update(invenbuyer);
                        }
                        _unitOfWork.MarketTransactionRepository.Create(transaction);

                        if(invenseller!= null)
                        {
                            _unitOfWork.InventoryRepository.Update(invenseller);
                        }
                        _unitOfWork.UserRepository.Update(buyer);
                        _unitOfWork.UserRepository.Update(seller);
                        _unitOfWork.SaveChange();
                    }
                    else
                    {
                        _notyfService.Warning("Please add for fund");
                        return RedirectToAction("Dashboard", "Users");
                    }
                }
                catch (Exception ex)
                {
                    _notyfService.Success("unsuccessful");
                    return RedirectToRoute("ListItem");
                }
            }
            _notyfService.Success("successful");
            return RedirectToRoute("ListItem");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.ItemRepository.getItemSellbyId((int)id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

    }
}
