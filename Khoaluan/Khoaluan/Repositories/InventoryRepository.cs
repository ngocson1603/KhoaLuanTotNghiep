using Dapper;
using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class InventoryRepository:GameStoreRepository<Inventory>,IInventoryRepository
    {
        public InventoryRepository(GameStoreDbContext context):base(context)
        {

        }

        public void updateInventory(int userID, int ItemID, int type)
        {
            var query = @"select * from inventory where UserID=@userid and ItemID=@itemid";
            var deletequery = @"delete from inventory where UserID=@userid and ItemID=@itemid";
            var parameter = new DynamicParameters();
            parameter.Add("userid", userID);
            parameter.Add("itemid", ItemID);
            var data=Context.Database.GetDbConnection().Query<Inventory>(query,parameter);
            var userItem = data.Single();
            if(type==(int)marketType.buy)
            {
                this.Create(userItem);
            }
            else if(type==(int)marketType.sell)
            {
                Context.Database.GetDbConnection().Execute(deletequery,parameter);
            }
        }
    }
}
