using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Khoaluan
{
    public interface IMongoContext: IDisposable 
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
