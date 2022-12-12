using Khoaluan.Areas.Admin.Models;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.Repositories
{
    public class ProductCategoryRepository:GameStoreRepository<ProductCategory>,IProductCategoryRepository
    {
        public ProductCategoryRepository(GameStoreDbContext context):base(context)
        {

        }
        public void updateCategory(int proId, PostSelectedViewModel cate)
        {
            List<ProductCategory> categories = new List<ProductCategory>();
            foreach (var p in cate.SelectedIds)
            {
                ProductCategory category = new ProductCategory()
                {
                    ProductId = proId,
                    CategoryId = p,
                };
                categories.Add(category);
            }
            this.BulkInsert(categories);
        }
    }
}
