using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IOrderRepository:IGameStoreRepository<Order>
    {
        Order createOrder();
    }
}
