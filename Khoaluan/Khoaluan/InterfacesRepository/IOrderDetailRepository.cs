using Khoaluan.Models;
using Khoaluan.ModelViews;
using System.Collections.Generic;

namespace Khoaluan.InterfacesRepository
{
    public interface IOrderDetailRepository:IGameStoreRepository<OrderDetail>
    {
        List<XemDonHang> getorder(int id);
    }
}
