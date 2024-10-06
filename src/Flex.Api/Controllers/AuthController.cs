using AutoMapper;
using Flex.Core.Contracts.Services;
using Flex.Core.Domain.Identity;
using Flex.Core.Exceptions;
using Flex.Core.Models.Common;
using Flex.Core.Models.Identity;
using Flex.Core.Shared.Constants.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Flex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        
        public AuthController(IAuthService authService,IMapper mapper,UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);

            return Ok(Result.Success("Đăng nhập thành công!", result));
        }

        [HttpPost("user-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var username = User.FindFirst(ClaimType.UserName)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedException("Người dùng chưa đăng nhập!");
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                throw new ArgumentException("Người dùng không tồn tại!");
            }

            var userProfile = _mapper.Map<UserInfo>(user);

            return Ok(Result.Success("Lấy thông tin người dùng thành công!", userProfile));
        }
    }
}
