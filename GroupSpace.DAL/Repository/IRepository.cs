using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.DAL.Repository
{
    public interface IRepository <T> 
    {
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);

        void Delete(T entity);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        bool CheckIfEntityExist(int id);
        void SaveChanges();
    }
}
