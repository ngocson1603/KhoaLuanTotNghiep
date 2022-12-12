using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.InterfacesRepository
{
    public interface ISaleProductRepository:IGameStoreRepository<SaleProduct>
    {
        List<int> currentSaleProduct();
    }
}
