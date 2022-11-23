using Khoaluan.Models;

namespace Khoaluan.InterfacesService
{
    public interface IUserService
    {
        Users updateBalance(int userID, int price, int type);
    }
}
