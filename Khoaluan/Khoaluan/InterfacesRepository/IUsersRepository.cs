using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IUsersRepository:IGameStoreRepository<Users>
    {
        void updateBalance(int userID, int price, int type);
    }
}
