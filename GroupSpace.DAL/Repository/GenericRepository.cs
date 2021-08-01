using GroupSpace.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected AppDataContext context;

        public GenericRepository(AppDataContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }
        

        public virtual T Get(int id)
        {
            return context.Find<T>(id);
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> All()
        {
            return context.Set<T>()
                .AsQueryable()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public bool CheckIfEntityExist(int id)
        {
            var entity = context.Find<T>(id);
                  
            if (entity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(T entity)
        { 
            context.Remove<T>(entity);           
        }

    }
}
