using Khoaluan.Areas.Admin.Models;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.Repositories
{
    public class CategoryRepository:GameStoreRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
