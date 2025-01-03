using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.Abstractions
{
    public interface IRepository<T> 
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task Add(T items);
        public Task Update(T items);
        public Task Delete(T items);


    }
   
    
}
