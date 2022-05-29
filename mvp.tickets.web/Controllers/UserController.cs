using Microsoft.AspNetCore.Mvc;
using mvp.tickets.domain.Models;
using mvp.tickets.domain.Services;

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

        [HttpGet("report")]
        public async Task<IBaseReportQueryResponse<IUserReportModel>> GetUsersReport([FromQuery] BaseReportQueryRequest request)
        {
            return await _userService.GetUsersReport(request);
        }
    }
}