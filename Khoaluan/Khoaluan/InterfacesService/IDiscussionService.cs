using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.InterfacesService
{
    public interface IDiscussionService
    {
        int comment(string postID, string userName, string message);
        List<Discussion> listDiscussion(int id);
    }
}
