using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;

namespace Khoaluan.Repositories
{
    public class MarketRepository:GameStoreRepository<Market>,IMarketRepository
    {
        public MarketRepository(GameStoreDbContext context):base(context)
        {

        }

        public void transaction(int userID, int itemID, int price,int type)
        {
            Market cmd=new Market()
            {
                UserID=userID,
                ItemID=itemID,
                Price=price,
                DayCreate=DateTime.Now,
                Status=type
            };
            this.Update(cmd);
        }
    }
}
