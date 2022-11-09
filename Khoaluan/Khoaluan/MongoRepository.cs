using Khoaluan.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using static Dapper.SqlMapper;

namespace Khoaluan
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<T> Dbset;
        public MongoRepository(IMongoContext context)
        {
            database = context.Database;
            Dbset = database.GetCollection<T>(typeof(T).Name);
        }
        public void add(T entity)
        {
            Dbset.InsertOne(entity);
        }

        public void Delete(string id)
        {
            var obj = Dbset.DeleteOne(FilterId(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<T> getall()
        {
            return Dbset.Find(Builders<T>.Filter.Empty).ToList();
        }

        public T getbyID(string id)
        {
            return Dbset.Find(FilterId(id)).SingleOrDefault();
        }
        private static FilterDefinition<T> FilterId(string key)
        {
            return Builders<T>.Filter.Eq("Id", key);
        }
        public void Update(string id, T entity)
        {
            Dbset.ReplaceOne(FilterId(id),entity);
        }
    }
}
