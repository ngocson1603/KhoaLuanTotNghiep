using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesRepository;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;
using System.Linq;
namespace Khoaluan.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        public RefundService(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        public Refund refund(int userID, int productID)
        {
            RefundRequest request=_refundRepository.lastestOrder(productID,userID);
            Refund rf=new Refund()
            {
                UserID=userID,
                OrderID=request.OrderId,
                ProductID=productID,
                Price=request.Price,
                Status=(int)RefundType.pending,
                DatePurchase=request.DatePurchase
            };
            return rf;
        }
    }
}
