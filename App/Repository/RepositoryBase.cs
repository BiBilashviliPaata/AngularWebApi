using App.Interface;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace App.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _db { get; set; }

        public RepositoryBase(AppDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<T> FindAll()
        {
            return this._db.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._db.Set<T>().Where(expression);
        }

        public T Get(int id)
        {
            return this._db.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            this._db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this._db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this._db.Set<T>().Remove(entity);
        }

    }
}
