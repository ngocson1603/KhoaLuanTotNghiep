using System;
using System.Collections.Generic;
namespace Khoaluan
{
    public interface IGameStoreRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        void BulkInsert(List<T> entities);

        void BulkUpdate(List<T> entities);

        void BulkDelete(List<T> entities);
    }
}
