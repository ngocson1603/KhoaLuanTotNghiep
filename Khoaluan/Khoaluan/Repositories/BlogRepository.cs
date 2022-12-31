using Khoaluan.Interfaces;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class BlogRepository : GameStoreRepository<Blog>, IBlogRepository
    {
        public BlogRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
