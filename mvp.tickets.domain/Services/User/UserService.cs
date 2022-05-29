using Microsoft.Extensions.Logging;
using mvp.tickets.domain.Enums;
using mvp.tickets.domain.Extensions;
using mvp.tickets.domain.Models;
using mvp.tickets.domain.Stores;

namespace mvp.tickets.domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserStore _userStore;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserStore userStore, ILogger<UserService> logger)
        {
            _userStore = userStore;
            _logger = logger;
        }

        public async Task<IBaseReportQueryResponse<IUserReportModel>> GetUsersReport(IBaseReportQueryRequest request)
        {
            if (request == null)
            {
                return new BaseReportQueryResponse<IUserReportModel>
                {
                    IsSuccess = false,
                    Code = ResponseCodes.BadRequest
                };
            }

            IBaseReportQueryResponse<IUserReportModel> response = default;

            try
            {
                response = await _userStore.GetUsersReport(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseReportQueryResponse<IUserReportModel>();
                response.HandleException(ex);
            }
            return response;
        }
    }
}
