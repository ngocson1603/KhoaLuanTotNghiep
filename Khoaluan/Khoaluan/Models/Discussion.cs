using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Models
{
    public class Discussion
    {
        public int GameId { get; set; }
        public int Username { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
