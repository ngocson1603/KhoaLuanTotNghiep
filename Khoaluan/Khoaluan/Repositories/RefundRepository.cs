using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Khoaluan.Repositories
{
    public class RefundRepository:GameStoreRepository<Models.Refund>,IRefundRepository
    {
        private readonly IUsersRepository _usersRepository;
        public RefundRepository(GameStoreDbContext context,IUsersRepository usersRepository):base(context)
        {
            _usersRepository = usersRepository;
        }
        public OtpModels.RefundRequest refundRequest(int productID, int UserID)
        {
            var query = @"select top 1.[o].id as OrderID,UserID,ProductID,Price,DatePurchase from [Order] o,OrderDetail od
                        where o.Id=od.Id and ProductID=@productid AND UserID=@userid
                        order by DatePurchase desc";
            var parameter = new DynamicParameters();
            parameter.Add("productid",productID);
            parameter.Add("userid", UserID);
            var request = Context.Database.GetDbConnection().QuerySingle<OtpModels.RefundRequest>(query, parameter);
            return request;
        }
        public List<gameRefund> listgameRefund(int userid)
        {
            var query = @"select l.ProductId,Name,Image from 
                        Product p,Library l,
						(select ProductID,UserID,MAX(DatePurchase) as latestDate from 
                        [Order] o,OrderDetail od
                        where o.Id=od.Id and UserID=@userid
                        group by ProductID,UserID)

						as tmp
                        where p.Id=tmp.ProductID and p.Id=l.ProductId and DATEDIFF(day,tmp.latestDate,GETDATE())<=7";
            var parameter = new DynamicParameters();
            parameter.Add("userid", userid);
            var result=Context.Database.GetDbConnection().Query<gameRefund>(query, parameter);
            return result.ToList();
        }
        public List<RefundUser> GetRefundUsers()
        {
            var query = @"select Id,UserID,Price from Refund where DATEDIFF(day,datecreate,getdate())=7 and Status=0";
            var result=Context.Database.GetDbConnection().Query<RefundUser>(query);
            return result.ToList();
        }
    }
}
