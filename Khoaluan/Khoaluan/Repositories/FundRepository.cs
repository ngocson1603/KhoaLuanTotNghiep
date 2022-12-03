using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class FundRepository : GameStoreRepository<Fund>, IFundRepository
    {
        public FundRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
