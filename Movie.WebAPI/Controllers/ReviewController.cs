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
    public class ReviewController : ControllerBase
    {
        public readonly IUnitOfWork _unit;
        public readonly IMapper _mapper;

        public ReviewController(IUnitOfWork unit, IMapper mapper)
        {
            _unit=unit;
            _mapper=mapper;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPost]   
        public async Task<IActionResult> AddAsync(ReviewDto review)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please Provide the Valid Data");
            }
            var mappedData=_mapper.Map<Review>(review);
            await  _unit.ReviewRepository.Add(mappedData);
            return Ok(mappedData);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviewData= await _unit.ReviewRepository.GetAll();
            if (reviewData == null)
            {
                return NotFound("The Data is not found");
            }
            return Ok(reviewData);
            
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("Review/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == null)
            {
                return BadRequest("Please give the Id");

            }
            var data=_unit.ReviewRepository.GetById(id);
            if(data == null)
            {
                return NotFound("Data does not exist");
            }
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int id, ReviewDto review)
        {
            if (id == null)
            {
                return BadRequest("Please give the Id");
            }
            var existingData = await _unit.ReviewRepository.GetById(id);
            if (existingData == null)
            {
                return NotFound("No Data found");
            }
            existingData.Date=review.Date;
            existingData.Movie = review.Movie;
            existingData.User = review.User;
            existingData.Comment = review.Comment;
            await _unit.ReviewRepository.Update(existingData);
            return Ok("Infomation Updated Successfully");

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Review/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == null)
            {
                return BadRequest("Please Give the Proper Id");
            }
            var review = await _unit.ReviewRepository.GetById(id);
            await _unit.ReviewRepository.Delete(review);
            return Ok("Deleted Successfully");
        }
    }
}
