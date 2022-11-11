using Khoaluan.Interfaces;
using Khoaluan.Models;

namespace Khoaluan.Repositories
{
    public class DiscussionRepository:MongoRepository<Discussion>,IDiscussionRepository
    {
        public DiscussionRepository(IMongoContext context):base(context)
        {

        }

        public void comment(string postID,string userName, string message)
        {
            var post=this.GetById(postID);
            Comment cmt=new Comment()
            {
                UserName=userName,
                Message=message,
            };
            post.Comments.Add(cmt);
            this.Update(postID,post);
        }
    }
}
