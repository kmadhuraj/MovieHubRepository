using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Abstractions;
using Movie.Application.DTO;
using Movie.Domain.Entities;

namespace Movie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public readonly IUnitOfWork _unit;
        public readonly IMapper _mapper;
        public GenreController(IUnitOfWork unit,IMapper mapper)
        {
            _unit= unit;
            _mapper= mapper;
        }
        [Authorize(Roles ="Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reviews=await _unit.GenreRepository.GetAll();
            if (reviews == null)
            {
                return NotFound("No Data Found");
            }
            return Ok(reviews);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("Genre/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id Not Found");
            }
            var genre=_unit.GenreRepository.GetById(id);
            return Ok(genre);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(GenreDto genre)
        {
            if(genre == null)
            {
                return BadRequest("Please Give Valid Data");
            }
            var mappedData=_mapper.Map<Genre>(genre);
            await _unit.GenreRepository.Add(mappedData);
            return Ok(mappedData);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Genre/{id}")]
        public async Task<IActionResult>UpdateAsync(int id,GenreDto genre)
        {
            if (id == 0 && genre==null)
            {
                return BadRequest("Please Give the Valid Data");
            }
            var existingData = await _unit.GenreRepository.GetById(id);
            existingData.Name = genre.Name;
            await _unit.GenreRepository.Update(existingData);
            return Ok("Information Updated Successfully");

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Genre/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _unit.GenreRepository.GetById(id);
            await _unit.GenreRepository.Delete(data);
            return Ok("Deleted Successfully");

        }

    }
}
