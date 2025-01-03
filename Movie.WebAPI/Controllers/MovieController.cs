using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Abstractions;
using Movie.Application.DTO;
using Movie.Domain.Entities;
using Movie.Infrastucture.Repositories;

namespace Movie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404, "Please give the valid data");
            }
            var mappedData = _mapper.Map<MovieModel>(movie);
            await _unitOfWork.MovieRepository.Add(mappedData);
            return StatusCode(201, "Movie added successfully");
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetMovie()
        {
            var Data = await _unitOfWork.MovieRepository.GetAll();
            if (Data == null)
            {
                return StatusCode(404, "This data does not exist");
            }
            return StatusCode(200, Data);


        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("Movie/{id}")]
        public async Task <IActionResult>GetMovieById(int id)
        {
            if (id == null)
            {
                return StatusCode(404, "Please Provide Correct Id");
            }
            var movie=await _unitOfWork.MovieRepository.GetById(id);
            return StatusCode(200,movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Movie/{id}")]
        public async Task<IActionResult>UpdateMovie(int id,MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404, "Please Provide Valid Data");
            }
            var existingMovie=await _unitOfWork.MovieRepository.GetById(id);
            existingMovie.Director = movie.Director;
            existingMovie.Title = movie.Title;  
            existingMovie.Duration= movie.Duration;
            existingMovie.PosterURL = movie.PosterURL;
            existingMovie.ReleaseDate= movie.ReleaseDate;
            await _unitOfWork.MovieRepository.Update(existingMovie);
            return StatusCode(200,"Information Updated Successfully");
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Movie/{id}")]
        public async Task<IActionResult>DeleteMovie(int id)
        {
            if (id == null)
            {
                return StatusCode(404, "Please Enter the Valid Data");
            }
            var movie=await _unitOfWork.MovieRepository.GetById(id);
            await _unitOfWork.MovieRepository.Delete(movie);
            return Ok("Deletion Successfull");
        }

    }
}
