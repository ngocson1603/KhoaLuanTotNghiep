using Khoaluan.Models;
using Khoaluan.OtpModels;

namespace Khoaluan.Interfaces
{
    public interface IRefundRepository:IGameStoreRepository<Refund>
    {
        OtpModels.RefundRequest refundRequest(int UserID, int productID);
    }
}
