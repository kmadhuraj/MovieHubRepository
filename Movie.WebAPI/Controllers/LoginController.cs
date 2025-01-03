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
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IAuthService _auth;
        private IMapper _mapper;
        public LoginController(IConfiguration config, IAuthService auth, IMapper mapper)
        {
            _config = config;
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)                                                                                                                                                                                                                                
        {
            IActionResult response = Unauthorized();
            var mappedUser = _mapper.Map<User>(user);
            var authenticatedUser = await _auth.Authenticate(mappedUser);                                                                                                             
            if (authenticatedUser == null)
            {
                return NotFound("The User does not exist!!");
            }
            var token = _auth.JwtToken(authenticatedUser);
            response = Ok(new
            {
                token = token
            });                             
            return Ok(response);                    
        }

    }
}
