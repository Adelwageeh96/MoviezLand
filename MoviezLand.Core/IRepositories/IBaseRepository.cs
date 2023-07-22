using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
         IEnumerable<T> GetAll();
         void Add(T item);
         
         T FindById(int id);
    }
}
