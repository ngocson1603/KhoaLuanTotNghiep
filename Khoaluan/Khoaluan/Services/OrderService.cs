using Khoaluan.InterfacesService;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Services
{
    public class OrderService : IOrderService
    {
        public Order createOrder(int userID, List<Cart> productPurchase)
        {
            List<OrderDetail> detail = new List<OrderDetail>();
            foreach (var p in productPurchase)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductID = p.product.Id,
                    Price = p.product.Price
                };
                detail.Add(orderDetail);
            }
            Order order = new Order()
            {
                UserID = userID,
                DatePurchase = DateTime.Now,
                TotalPrice = productPurchase.Sum(t => t.product.Price),
                OrderDetails = detail
            };
            return order;
        }
    }
}
