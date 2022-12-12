using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;

namespace Khoaluan.Repositories
{
    public class DeveloperRepository:GameStoreRepository<Developer>,IDeveloperRepository
    {
        public DeveloperRepository(GameStoreDbContext context):base(context)
        {

        }
        public Developer getDev(string id)
        {
            var query = @"select * from Developer where UserName=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().QuerySingle<Developer>(query, parameter);
            return data;
        }
    }
}
