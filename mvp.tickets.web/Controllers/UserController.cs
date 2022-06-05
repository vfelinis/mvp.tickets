using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using mvp.tickets.domain.Models;
using mvp.tickets.domain.Services;
using System.Security.Claims;

namespace mvp.tickets.web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("current")]
        public async Task<IBaseQueryResponse<IUserModel>> Current()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
                var response = await _userService.Query(new UserQueryRequest { Id = id });
                return response;
            }
            else
            {
                return new BaseQueryResponse<IUserModel>
                {
                    IsSuccess = true,
                    Code = domain.Enums.ResponseCodes.Success,
                    Data = null
                };
            }
        }

        [HttpPost("login")]
        public async Task<IBaseCommandResponse<IUserModel>> Login(UserLoginCommandRequest request)
        {
            var response = await _userService.Login(request);
            if (response.IsSuccess)
            {
                var claimsIdentity = new ClaimsIdentity(response.Data.claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            return new BaseCommandResponse<IUserModel>
            {
                IsSuccess = response.IsSuccess,
                Code = response.Code,
                ErrorMessage = response.ErrorMessage,
                Data = response.Data.user
            };
        }

        [HttpPost("logout")]
        public async Task<IBaseCommandResponse<object>> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new BaseCommandResponse<object>
            {
                IsSuccess = true,
                Code = domain.Enums.ResponseCodes.Success
            };
        }

        [HttpGet("report")]
        public async Task<IBaseReportQueryResponse<IEnumerable<IUserModel>>> GetUsersReport([FromQuery] BaseReportQueryRequest request)
        {
            return await _userService.GetUsersReport(request);
        }
    }
}