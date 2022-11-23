using Khoaluan.Interfaces;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class CategoryRepository:GameStoreRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
