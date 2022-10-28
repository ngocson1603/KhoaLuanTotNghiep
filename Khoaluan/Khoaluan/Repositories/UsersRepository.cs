using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class UsersRepository : GameStoreRepository<Users>, IUsersRepository
    {
        public UsersRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
