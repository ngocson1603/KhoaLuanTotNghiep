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

        
    }
}
