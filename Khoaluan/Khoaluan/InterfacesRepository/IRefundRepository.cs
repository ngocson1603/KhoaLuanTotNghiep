using Khoaluan.Models;
using Khoaluan.OtpModels;

namespace Khoaluan.Interfaces
{
    public interface IRefundRepository:IGameStoreRepository<Refund>
    {
        void refund(int userID, int productID, int OrderID);
        int refundID(int userID, int productID);
        RefundRequest lastestOrder(int productId, int UserID);
    }
}
