using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IProductRepository:IGameStoreRepository<Product>
    {
        public List<Productdetail> getallProductwithCategory();
        public List<Product> GetProductByName(string name);
        public List<Product> listProductDev(int id);
        public ActiveGame listProdevActive(int id);
        public List<ActiveGame> listProdevNotif();
        public List<Product> listProductItem();
    }
}
