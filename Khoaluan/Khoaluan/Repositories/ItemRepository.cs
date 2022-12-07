using Dapper;
using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class ItemRepository : GameStoreRepository<Item>, IItemRepository
    {
        public ItemRepository(GameStoreDbContext context) : base(context)
        {

        }
        public List<ItemModelView> getItem(int id)
        {
            var query = @"select Item.Id as Id,Item.Name as Name,Item.Image as Image,Product.Name as ProductId,MinPrice,MaxPrice from Item, Product
                            where Item.ProductId = Product.Id and Product.DevId = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<ItemModelView>(query, parameter);
            return data.ToList();
        }
        public ItemModelView getItemById(int id)
        {
            var query = @"select Item.Id as Id,Item.Name as Name,Item.Image as Image,Product.Name as ProductId,MinPrice,MaxPrice from Item, Product
                            where Item.ProductId = Product.Id and Item.Id=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().QuerySingle<ItemModelView>(query, parameter);
            return data;
        }

        public List<Item> getItemByUser(int id)
        {
            var query = @"select Item.Id as Id,Item.Name,Item.Image,MinPrice,MaxPrice,Quantity from Item,Inventory where Item.Id = Inventory.ItemID and Inventory.UserID = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<Item>(query, parameter);
            return data.ToList();
        }

        public List<SellitemModelView> getItemSell()
        {
            var query = @"select Market.Id as Id, Item.Image as Image, PricePerItem, Market.Quantity as Quantity, Item.Name as NameItem, DayCreate, Market.UserID as UserId, HoTen, Product.Name as NameGame
from Inventory,Market,Item, Users, Product
where Inventory.ItemID = Market.ItemID and Market.ItemID = Item.Id and Inventory.UserID = Users.Id and Item.ProductId = Product.Id";
            var parameter = new DynamicParameters();
            var data = Context.Database.GetDbConnection().Query<SellitemModelView>(query, parameter);
            return data.ToList();
        }
        public SellitemModelView getItemSellbyId(int id)
        {
            var query = @"select Market.Id as Id, Item.Image as Image, PricePerItem, Market.Quantity as Quantity, Item.Name as NameItem, DayCreate, Market.UserID as UserId, HoTen, Product.Name as NameGame
from Inventory,Market,Item, Users, Product
where Inventory.ItemID = Market.ItemID and Market.ItemID = Item.Id and Inventory.UserID = Users.Id and Item.ProductId = Product.Id and Market.Id = @id ";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().QuerySingle<SellitemModelView>(query, parameter);
            return data;
        }
    }
}
