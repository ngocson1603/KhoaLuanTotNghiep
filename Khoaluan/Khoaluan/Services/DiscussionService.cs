using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;

namespace Khoaluan.Services
{
    public class DiscussionService : IDiscussionService
    {
        public IDiscussionRepository _discussionRepository { get; set; }
        public DiscussionService(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }
        public void comment(string postID, string userName, string message)
        {
            var post = _discussionRepository.GetById(postID);
            Comment cmt = new Comment()
            {
                UserName = userName,
                Message = message,
            };
            post.Comments.Add(cmt);
            _discussionRepository.Update(postID, post);
        }
    }
}
