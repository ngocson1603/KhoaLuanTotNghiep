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
        public Order createOrder()
        {
            var item = _productRepository.GetAll().Take(3);
            List<OrderDetail> detail = new List<OrderDetail>();
            foreach(var p in item)
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
                UserID = 2,
                DatePurchase = DateTime.Now,
                TotalPrice = item.Sum(t => t.Price),
                OrderDetails = detail
            };
            return order;
        }
    }
}
