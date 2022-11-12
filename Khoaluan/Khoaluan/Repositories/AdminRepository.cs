using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class AdminRepository : GameStoreRepository<Admin>, IAdminRepository
    {
        public AdminRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
