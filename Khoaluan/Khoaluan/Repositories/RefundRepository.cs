using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;

namespace Khoaluan.Repositories
{
    public class RefundRepository:GameStoreRepository<Refund>,IRefundRepository
    {
        private readonly IUsersRepository _usersRepository;
        public RefundRepository(GameStoreDbContext context,IUsersRepository usersRepository):base(context)
        {
            _usersRepository = usersRepository;
        }

        public void refund(int userID, int productID, int OrderID)
        {
            var query = @"select price from [order],orderdetail
                        where [order].id=orderdetail.id
                        and [order].id=@orderid and productid=@productid";
            var parameter = new DynamicParameters();
            parameter.Add("orderid", OrderID);
            parameter.Add("productid", productID);
            var price = Context.Database.GetDbConnection().QuerySingle<int>(query, parameter);
            var user = _usersRepository.GetById(userID);
            user.Balance = user.Balance + price;
            _usersRepository.Update(user);
        }

        public int refundID(int userID, int productID)
        {
            var query = @"select top 1.[Order].Id from [Order], OrderDetail 
                        where [Order].Id = OrderDetail.Id and UserID = @userID and ProductID = @productID
                        order by DatePurchase Desc";
            var parameter = new DynamicParameters();
            parameter.Add("userID", userID);
            parameter.Add("productID", productID);
            int id = Context.Database.GetDbConnection().QuerySingle<int>(query, parameter);
            return id;
        }
    }
}
