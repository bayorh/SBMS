using Microsoft.AspNetCore.Mvc;
using SBMS.src.Contracts;
using SBMS.src.Dtos;
using System.Threading.Tasks;

namespace SBMS.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(request);

            if (result == null || !result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}