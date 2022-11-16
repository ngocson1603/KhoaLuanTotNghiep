using Khoaluan.Interfaces;
using Khoaluan.InterfacesRepository;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using System.Collections.Generic;
using System.Linq;
namespace Khoaluan.Services
{
    public class RefundService : IRefundService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IRefundRepository _refundRepository;
        private readonly IUsersRepository _usersRepository;
        public RefundService(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        public Users refund(int userID, int productID)
        {
            var orderID=_refundRepository.refundID(userID, productID);
            var orders=_orderRepository.GetAll();
            var orderdetails = _orderDetailRepository.GetAll();
            var result = (from o in orders
                        join ots in orderdetails
                        on o.Id equals ots.Id
                        where o.Id == orderID && ots.ProductID == productID
                        select new
                        {
                            Price = ots.Price
                        }).Single();
            int price = result.Price;
            var user =_usersRepository.GetById(userID);
            user.Balance = user.Balance + price;
            return user;
        }
    }
}
