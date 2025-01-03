using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Application.Abstractions;
using Movie.Domain.Entities;
using Movie.Infrastucture.Context;

namespace Movie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRegistrationController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly ApplicationDbContext _context;
        public AdminRegistrationController(IAuthService auth, ApplicationDbContext context)
        {
            _context = context;
            _auth = auth;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(User user)
        {
            if(user == null)
            {
                return BadRequest("User cant be null");
            }
            try
            {
                var response = _auth.RegisterOrUpdateAdmin(user);
                return Ok(response);
            }
            catch(Exception ex)
            {
               return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        
        }


    }
}
