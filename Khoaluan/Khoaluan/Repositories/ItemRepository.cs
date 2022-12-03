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
        public List<ItemModelView> getItem()
        {
            var query = @"select Item.Id as Id,Item.Name as Name,Item.Image as Image,Product.Name as ProductId,MinPrice,MaxPrice from Item, Product
                            where Item.ProductId = Product.Id";
            var parameter = new DynamicParameters();
            var data = Context.Database.GetDbConnection().Query<ItemModelView>(query, parameter);
            return data.ToList();
        }
    }
}
