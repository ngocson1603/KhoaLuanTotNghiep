using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IProductRepository:IGameStoreRepository<Product>
    {
        public List<Productdetail> getallProductwithCategory();
    }
}
