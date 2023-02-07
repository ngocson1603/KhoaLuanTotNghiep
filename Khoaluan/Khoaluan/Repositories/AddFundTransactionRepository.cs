using Khoaluan.Interfaces;
using Khoaluan.Models;
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
    }
}
