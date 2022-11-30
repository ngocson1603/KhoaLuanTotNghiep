using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IMarketRepository:IGameStoreRepository<Market>
    {
        List<Market> getlistMarket(int MarketType);
    }
}
