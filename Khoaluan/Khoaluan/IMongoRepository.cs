using System;
using System.Collections.Generic;

namespace Khoaluan
{
    public interface IMongoRepository<T>:IDisposable where T : class
    {
        void add(T entity);
        T getbyID(string id);
        List<T> getall();
        void Update(string id,T entity);
        void Delete(string id);
    }
}
