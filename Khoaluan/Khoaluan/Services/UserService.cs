using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;

namespace Khoaluan.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Users updateBalance(int userID, int price, int type)
        {
            Users user = _usersRepository.GetById(userID);
            if (type == (int)marketType.buy)
            {
                user.Balance = user.Balance - price;
            }
            else if (type == (int)marketType.sell)
            {
                user.Balance = user.Balance + price;
            }
            return user;
        }
    }
}
