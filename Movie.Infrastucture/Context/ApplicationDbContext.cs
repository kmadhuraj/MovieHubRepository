using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastucture.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieGenre>  MoviesGenre { get; set;}

    }
}
