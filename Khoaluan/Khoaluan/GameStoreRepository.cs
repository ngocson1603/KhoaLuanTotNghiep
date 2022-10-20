using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

namespace Khoaluan
{
    public class GameStoreRepository<T>:IGameStoreRepository<T> where T : class
    {
        protected readonly GameStoreDbContext Context;
        protected readonly DbSet<T> Table;  
        public GameStoreRepository(GameStoreDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }
        public List<T> GetAll()
        {
            return Table.ToList();
        }
        public T GetById(int id)
        {
            return Table.Find(id);
        }
        public void Create(T entity)
        {
            Table.Add(entity);
        }
        public void Update(T entity)
        {
            Table.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            T entity = Table.Find(id);
            Table.Remove(entity);
        }
        public void BulkInsert(List<T> entities)
        {
            Context.BulkInsert(entities);
        }

        public void BulkUpdate(List<T> entities)
        {
            Context.BulkUpdate(entities);
        }

        public void BulkDelete(List<T> entities)
        {
            Context.BulkDelete(entities);
        }
    }
}
