using mvp.tickets.domain.Models;

namespace mvp.tickets.domain.Stores
{
    public interface IUserStore
    {
        Task<IBaseReportQueryResponse<IUserReportModel>> GetUsersReport(IBaseReportQueryRequest request);
    }
}
