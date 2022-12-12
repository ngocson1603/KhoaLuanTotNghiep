using Khoaluan.Models;
using Khoaluan.OtpModels;

namespace Khoaluan.InterfacesService
{
    public interface ISaleService
    {
        Sale CreateSale(SaleRequest request,out string message);
    }
}
