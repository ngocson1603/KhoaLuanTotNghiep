using Khoaluan.InterfacesRepository;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class OrderDetailRepository:GameStoreRepository<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
