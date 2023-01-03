using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IProductRepository:IGameStoreRepository<Product>
    {
        List<Productdetail> getallProductwithCategory();
        List<Productdetail1> getallProductwithCategory1();
        List<Product> GetProductByName(string name);
        List<Product> listProductDev(int id);
        ActiveGame listProdevActive(int id);
        List<ActiveGame> listProdevNotif();
        List<Product> listProductItem();
        List<ActiveGame> listProforum();
        List<Product> listProductItem(int id);
        List<Product> listProductRelease();
    }
}
