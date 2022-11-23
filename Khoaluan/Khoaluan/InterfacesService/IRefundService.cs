using Khoaluan.Models;

namespace Khoaluan.InterfacesService
{
    public interface IRefundService
    {
        Users refund(int userID, int productID);
    }
}
