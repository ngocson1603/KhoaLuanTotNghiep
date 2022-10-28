using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class UserRepository : GameStoreRepository<Users>, IUserRepository
    {
        public UserRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
