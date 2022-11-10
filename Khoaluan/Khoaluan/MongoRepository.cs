using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using static Dapper.SqlMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;

namespace Khoaluan
{
    public abstract class MongoRepository<T>: IMongoRepository<T> where T : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<T> DbSet;
        protected MongoRepository(IMongoContext context)
        {
            Context = context;
            DbSet = Context.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T obj)
        {
            DbSet.InsertOne(obj);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public List<T> GetAll()
        {
            return DbSet.Find(Builders<T>.Filter.Empty).ToList();
        }

        public T GetById(string id)
        {
            return DbSet.Find(FilterId(id)).SingleOrDefault();
        }

        public void Remove(string id)
        {
            DbSet.DeleteOne(FilterId(id));
        }

        public void Update(string id, T obj)
        {
            DbSet.ReplaceOne(FilterId(id), obj);
        }
        private static FilterDefinition<T> FilterId(string key)
        {
            return Builders<T>.Filter.Eq("postID",key);
        }
    }
}
