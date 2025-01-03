using Movie.Application.DTO;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Genre> GenreRepository { get; set; }
        IRepository<MovieModel> MovieRepository { get; set; }
        IRepository<Review> ReviewRepository { get; set; }  
        IRepository<MovieGenre> MovieGenreRepository { get; set; }
    }
}
