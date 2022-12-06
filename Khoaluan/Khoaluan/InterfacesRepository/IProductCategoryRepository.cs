using Khoaluan.Areas.Admin.Models;
using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IProductCategoryRepository:IGameStoreRepository<ProductCategory>
    {
        void updateCategory(int proId, PostSelectedViewModel cate);
    }
}
