using Microsoft.EntityFrameworkCore;
using Movie.Application.Abstractions;
using Movie.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastucture.Repositories
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }

        public async Task Add(T items)
        {
            _dbSet.Add(items);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T items)
        {
            _dbSet.Remove(items);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<T> GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async  Task Update(T items)
        {
            
            _dbSet.Update(items);
            await _context.SaveChangesAsync();
        }

       
    }
}
