using Khoaluan.Models;
using Khoaluan.ModelViews;
using System.Collections.Generic;

namespace Khoaluan.InterfacesRepository
{
    public interface ISaleProductRepository:IGameStoreRepository<SaleProduct>
    {
        List<int> currentSaleProduct();
        int getdiscount(int productid);
        List<SaleModelView> ProductIsSale(int id);
        List<SaleModelView> ProductIsSaleInDate();
        List<SaleModelView> ProductNotSale();
        List<SaleModelView> ProductSaleHomePage();
        List<SellitemModelView> ProductSellInMonth();
    }
}
