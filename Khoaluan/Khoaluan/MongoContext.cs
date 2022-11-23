using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Khoaluan
{
    public class MongoContext:IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }
        
        private readonly IConfiguration _configuration;
        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;          
            
        }
        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }            
            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

            Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {           
            GC.SuppressFinalize(this);
        }      
    }
}
