using Khoaluan.Models;

namespace Khoaluan.InterfacesService
{
    public interface IRefundService
    {
        Refund refund(int userID, int productID);
    }
}
