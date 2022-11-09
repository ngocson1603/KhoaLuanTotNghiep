using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class OrderRepository:GameStoreRepository<Order>, IOrderRepository
    {
        private readonly IProductRepository _productRepository;
        public OrderRepository(GameStoreDbContext context,IProductRepository productRepository):base(context)
        {
            _productRepository=productRepository;
        }
        public Order createOrder(int userID, List<Product> productPurchase)
        {
            List<OrderDetail> detail = new List<OrderDetail>();
            foreach(var p in productPurchase)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductID = p.Id,
                    Price=p.Price
                };
                detail.Add(orderDetail);    
            }
            Order order = new Order()
            {
                UserID = userID,
                DatePurchase = DateTime.Now,
                TotalPrice = productPurchase.Sum(t => t.Price),
                OrderDetails = detail
            };
            return order;
        }
    }
}
