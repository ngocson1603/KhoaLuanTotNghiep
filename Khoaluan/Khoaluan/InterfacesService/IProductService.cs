using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.InterfacesService
{
    public interface IProductService
    {
        void ReleaseProduct();
        bool IsGameCanBuy(int UserId,int ProductId);
    }
}
