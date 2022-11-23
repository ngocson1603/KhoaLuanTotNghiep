using System;
using System.Collections.Generic;
using static Dapper.SqlMapper;
using System.Threading.Tasks;

namespace Khoaluan
{
    public interface IMongoRepository<T>: IDisposable where T : class
    {
        void Add(T obj);
        T GetById(string id);
        List<T> GetAll();
        void Update(string id,T obj);
        void Remove(string id);
    }
}
