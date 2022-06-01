using Dapper;
using Microsoft.Data.SqlClient;
using mvp.tickets.data.Procedures;
using mvp.tickets.domain.Constants;
using mvp.tickets.domain.Enums;
using mvp.tickets.domain.Helpers;
using mvp.tickets.domain.Models;
using mvp.tickets.domain.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvp.tickets.data.Stores
{
    public class UserStore : IUserStore
    {
        private readonly IConnectionStrings _connectionStrings;

        public UserStore(IConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings ?? ThrowHelper.ArgumentNull<IConnectionStrings>();
        }

        public async Task<IBaseReportQueryResponse<IUserReportModel>> GetUsersReport(IBaseReportQueryRequest request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(UsersReportProcedure.Params.Offset, request.Offset ?? 0, DbType.Int32);
                parameter.Add(UsersReportProcedure.Params.Limit, request.Limmit ?? ReportConstants.DEFAULT_LIMIT, DbType.Int32);

                using (var multi = await connection.QueryMultipleAsync(UsersReportProcedure.Name, param: parameter,
                    commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new BaseReportQueryResponse<IUserReportModel>
                    {
                        Data = multi.Read<UserReportModel>().ToList(),
                        Total = multi.Read<int>().FirstOrDefault(),
                        IsSuccess = true,
                        Code = ResponseCodes.Success
                    };
                }
            }
        }
    }
}
