using MongoDB.Driver;

namespace Khoaluan
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
    public abstract class MongodbContext:IMongoContext
    {
        protected MongodbContext(IMongoSetting connectionSetting)
        {
            var client = new MongoClient(connectionSetting.ConnectionString);
            Database = client.GetDatabase(connectionSetting.DatabaseName);
        }
        public IMongoDatabase Database { get; }
    }
}
