using Khoaluan.InterfacesRepository;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class MarketTransactionRepository:GameStoreRepository<MarketTransaction>,IMarketTransactionRepository
    {
        public MarketTransactionRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
