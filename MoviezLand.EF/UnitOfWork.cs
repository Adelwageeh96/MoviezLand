﻿using MoviezLand.Core;
using MoviezLand.Core.IRepositories;
using MoviezLand.Core.IRepository;
using MoviezLand.Core.Models;
using MoviezLand.EF.Data;
using MoviezLand.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IBaseRepository<Movie> Movies { get; private set; }
        public IGenreRepository Genres { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Movies=new BaseRepository<Movie>(context);
            Genres= new GenreRepository(context);
        }

        public int Complete()=> context.SaveChanges();

        public void Dispose()=> context.Dispose();
    }
}