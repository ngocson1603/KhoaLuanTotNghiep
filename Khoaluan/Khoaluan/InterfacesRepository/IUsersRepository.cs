using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IUsersRepository:IGameStoreRepository<Users>
    {
        void updateBalance(int userID, decimal price, int type);
        Users FindByEmail(string email);
    }
}
