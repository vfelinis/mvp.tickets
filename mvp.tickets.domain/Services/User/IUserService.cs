using mvp.tickets.domain.Models;

namespace mvp.tickets.domain.Services
{
    public interface IUserService
    {
        Task<IBaseReportQueryResponse<IUserReportModel>> GetUsersReport(IBaseReportQueryRequest request);
    }
}