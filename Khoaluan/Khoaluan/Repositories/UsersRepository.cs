using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class UsersRepository : GameStoreRepository<Users>, IUsersRepository
    {
        public UsersRepository(GameStoreDbContext context) : base(context)
        {

        }

        public void updateBalance(int userID,int price,int type)
        {
            Users user=this.GetById(userID);
            if(type==(int)marketType.buy)
            {
                user.Balance = user.Balance - price;
            }
            else if(type==(int)marketType.sell)
            {
                user.Balance=user.Balance+price;
            }
            this.Update(user);
        }
    }
}
