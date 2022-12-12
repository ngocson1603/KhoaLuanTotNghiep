using Khoaluan.InterfacesRepository;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class SaleRepository:GameStoreRepository<Sale>,ISaleRepository
    {
        public SaleRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
