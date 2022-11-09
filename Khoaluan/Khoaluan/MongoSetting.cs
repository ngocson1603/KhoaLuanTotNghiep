namespace Khoaluan
{
    public class MongoSetting : IMongoSetting
    {
        public string collectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
