using Khoaluan.Interfaces;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class ProductCategoryRepository:GameStoreRepository<ProductCategory>,IProductCategoryRepository
    {
        public ProductCategoryRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
