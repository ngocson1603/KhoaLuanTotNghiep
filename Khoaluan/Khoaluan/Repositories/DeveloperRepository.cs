using Khoaluan.Interfaces;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class DeveloperRepository:GameStoreRepository<Developer>,IDeveloperRepository
    {
        public DeveloperRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
