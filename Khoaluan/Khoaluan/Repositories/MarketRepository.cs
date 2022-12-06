﻿using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class MarketRepository:GameStoreRepository<Market>,IMarketRepository
    {
        public MarketRepository(GameStoreDbContext context):base(context)
        {

        }

        public List<Market> getlistMarket(int MarketType,int itemID)
        {
            var x=GetAll().Where(t=>(t.Status==MarketType)&& (t.ItemID==itemID)).ToList();
            return x;
        }    
    }
}
