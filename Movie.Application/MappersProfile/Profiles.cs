using AutoMapper;
using Movie.Application.DTO;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.MappersProfile
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<MovieDto,MovieModel>();
            CreateMap<ReviewDto,Review>();
            CreateMap<GenreDto,Genre>();
            CreateMap<UserDto, User>();
            CreateMap<UserRegistrationDto, User>();
        }
    }
    
}
