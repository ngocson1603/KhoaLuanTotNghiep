using Khoaluan.Interfaces;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class DiscussionRepository:MongoRepository<Discussion>,IDiscussionRepository
    {
        public DiscussionRepository(MongodbContext context):base(context)
        {

        }
    }
}
