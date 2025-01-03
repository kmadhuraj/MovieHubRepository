using Movie.Application.Abstractions;
using Movie.Application.DTO;
using Movie.Domain.Entities;
using Movie.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastucture.Repositories
{
    

    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GenreRepository=new Repository<Genre>(context);
            MovieRepository= new Repository<MovieModel>(context);
            ReviewRepository=new Repository<Review>(context);
            MovieGenreRepository=new Repository<MovieGenre>(context);
        }
        public IRepository<Genre> GenreRepository { get; set; }
        public IRepository<MovieModel> MovieRepository { get; set; }
        public IRepository<Review> ReviewRepository { get; set; }

        public IRepository<MovieGenre> MovieGenreRepository { get ; set ; }
    }
}

