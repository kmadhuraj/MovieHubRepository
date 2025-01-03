using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Abstractions;
using Movie.Application.DTO;
using Movie.Domain.Entities;

namespace Movie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _auth;

        public UserRegistrationController(IMapper mapper, IAuthService auth)
        {
            _auth = auth;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto user)
        {
            if (user == null)
            {
                return BadRequest("User can't be empty.");
            }
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mappedData = _mapper.Map<User>(user);
                var response = _auth.RegisterUser(mappedData);

                if (response == null)
                {
                    return BadRequest("Registration failed.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred during registration.");
            }
        }
    

    }

}
