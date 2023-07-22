using Microsoft.EntityFrameworkCore;
using MoviezLand.Core.IRepository;
using MoviezLand.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public  IEnumerable<T> GetAll() =>  context.Set<T>().ToList();

        
	}
}
