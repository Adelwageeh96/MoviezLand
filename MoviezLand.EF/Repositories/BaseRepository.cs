using Microsoft.EntityFrameworkCore;
using MoviezLand.Core.Constants;
using MoviezLand.Core.IRepository;
using MoviezLand.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context)
        {
                this.context = context;
        }

		public void Add(T item) => context.Add(item);

        public T FindById(int id) => context.Set<T>().Find(id);

		public  IEnumerable<T> GetAll(Expression<Func<T,string>>orderBy=null,string orderByDirection=OrderBy.Ascending)
        {
            IQueryable<T> query = context.Set<T>();
            if(orderBy is not null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query.OrderBy(orderBy);
                else
					query.OrderByDescending(orderBy);
			}
            return query.ToList();
		}

        public void RemoveRange(IEnumerable<T> models) => context.Set<T>().RemoveRange(models);
    }
}
