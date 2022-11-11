using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IDiscussionRepository:IMongoRepository<Discussion>
    {
        void comment(string postID, string userName,string message);
    }
}
