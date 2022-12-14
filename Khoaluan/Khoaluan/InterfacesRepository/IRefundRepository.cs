using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IRefundRepository:IGameStoreRepository<Refund>
    {
        OtpModels.RefundRequest refundRequest(int UserID, int productID);
        List<gameRefund> listgameRefund(int userid);
    }
}
