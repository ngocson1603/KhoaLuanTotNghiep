using Dapper;
using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public Users FindByEmail(string email)
        {
            var query = @"select * from Users where Gmail = @email";
            var para = new DynamicParameters();
            para.Add("email", email);

            try
            {
                return (Users)Context.Database.GetDbConnection().Query<Users>(query, para).First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void updateBalance(int userID,decimal price,int type)
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
            else if (type == (int)marketType.paypal)
            {
                user.Balance = user.Balance + price;
            }    
            this.Update(user);
        }
    }
}
