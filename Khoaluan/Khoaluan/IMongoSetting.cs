namespace Khoaluan
{
    public interface IMongoSetting
    {
        string collectionName { get; set; }
        string ConnectionString { get; set; }   
        string DatabaseName { get; set; }
    }
}
