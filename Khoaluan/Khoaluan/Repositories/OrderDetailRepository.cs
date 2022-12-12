using Dapper;
using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class OrderDetailRepository : GameStoreRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(GameStoreDbContext context) : base(context)
        {

        }
        public List<XemDonHang> getorder(int id)
        {
            var query = @"select OrderDetail.Id as Id,Product.Name as Name,OrderDetail.Price as Price,[Order].TotalPrice as TotalPrice from OrderDetail, Product, [Order]
            where [Order].Id = OrderDetail.Id and Product.Id = OrderDetail.ProductID and OrderDetail.Id = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<XemDonHang>(query, parameter);
            return data.ToList();
        }
    }
}
