using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Discussion
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string postID { get; set; }
        public int ProductID { get; set; }  
        public int UserName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
