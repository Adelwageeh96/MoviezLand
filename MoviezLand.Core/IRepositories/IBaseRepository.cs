using MoviezLand.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
         IEnumerable<T> GetAll(Expression<Func<T,string>>orderBy=null,string orderByDirection=OrderBy.Ascending);
         void Add(T item);
         
         T FindById(int id);

        void RemoveRange(IEnumerable<T> models);
    }
}
