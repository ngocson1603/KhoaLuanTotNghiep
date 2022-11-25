using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

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

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }   
            
            post.Comments.Add(cmt);
            _discussionRepository.Update(postID, post);
        }

        public List<Discussion> listDiscussion(int id)
        {
            return _discussionRepository.GetAll().Where(t => t.ProductID == id).ToList();
        }
    }
}
