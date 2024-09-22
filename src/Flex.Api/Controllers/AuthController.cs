using Flex.Core.Contracts.Services;
using Flex.Core.Models.Common;
using Flex.Core.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Flex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //var result = await _authService.Login(request);

            return Ok(Result.Success("ok",new List<int> { 1,2,3}));
        }
    }
}
