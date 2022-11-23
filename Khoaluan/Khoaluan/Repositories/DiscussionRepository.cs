using Khoaluan.Interfaces;
using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.Repositories
{
    public class DiscussionRepository:MongoRepository<Discussion>,IDiscussionRepository
    {
        public DiscussionRepository(IMongoContext context):base(context)
        {

        }        
    }
}
