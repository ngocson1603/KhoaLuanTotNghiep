using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class AddFundTransactionRepository : GameStoreRepository<AddFundTransaction>, IAddFundTransactionRepository
    {
        public AddFundTransactionRepository(GameStoreDbContext context) : base(context)
        {

        }
        public List<AddFundTransaction> GetFundTransactions(int id)
        {
            var query = @"select TransactionId,OrderId,Price as FundId,UserId,PaymentMethod,CreateOn 
                          from AddFundTransaction,Fund where 
                          AddFundTransaction.FundId = Fund.Id and AddFundTransaction.UserId = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<AddFundTransaction>(query, parameter);
            return data.ToList();
        }
    }
}
