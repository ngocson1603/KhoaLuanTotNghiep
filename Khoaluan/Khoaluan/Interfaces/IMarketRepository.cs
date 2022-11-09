using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IMarketRepository:IGameStoreRepository<Market>
    {
        public void transaction(int userID, int itemID, int price,int type);
    }
}
