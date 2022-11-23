using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IRefundRepository:IGameStoreRepository<Refund>
    {
        void refund(int userID, int productID, int OrderID);
        int refundID(int userID, int productID);
    }
}
