using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mvp.tickets.data.Models;
using mvp.tickets.data.Procedures;
using mvp.tickets.domain.Constants;
using mvp.tickets.domain.Enums;
using mvp.tickets.domain.Helpers;
using mvp.tickets.domain.Models;
using mvp.tickets.domain.Stores;
using System.Data;

namespace mvp.tickets.data.Stores
{
    public class UserStore : IUserStore
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConnectionStrings _connectionStrings;

        public UserStore(ApplicationDbContext dbContext, IConnectionStrings connectionStrings)
        {
            _dbContext = dbContext ?? ThrowHelper.ArgumentNull<ApplicationDbContext>();
            _connectionStrings = connectionStrings ?? ThrowHelper.ArgumentNull<IConnectionStrings>();
        }

        public async Task<IBaseCommandResponse<IUserModel>> Create(IUserCreateCommandRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsLocked = false,
                Permissions = request.Permissions,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now
            };
            await _dbContext.Users.AddAsync(user).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return new BaseCommandResponse<IUserModel>
            {
                Data = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsLocked = user.IsLocked,
                    Permissions = user.Permissions,
                    DateCreated = user.DateCreated,
                    DateModified = user.DateModified,
                },
                IsSuccess = true,
                Code = ResponseCodes.Success
            };
        }

        public async Task<IBaseQueryResponse<IUserModel>> Query(IUserQueryRequest request)
        {
            var response = new BaseReportQueryResponse<IUserModel>();
            User user = default;
            if (request?.Email != null)
            {
                user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Email == request.Email.ToLower()).ConfigureAwait(false);
            }
            else if (request?.Id != null)
            {
                user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id).ConfigureAwait(false);
            }

            if (user != null)
            {
                response.IsSuccess = true;
                response.Code = ResponseCodes.Success;
                response.Data = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Permissions = user.Permissions,
                    IsLocked = user.IsLocked,
                    DateCreated = user.DateCreated,
                    DateModified = user.DateModified,
                };
            }
            else
            {
                response.IsSuccess = false;
                response.Code = ResponseCodes.NotFound;
            }

            return response;
        }

        public async Task<IBaseReportQueryResponse<IEnumerable<IUserModel>>> GetUsers(IBaseReportQueryRequest request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(GetUsersProcedure.Params.Offset, request.Offset ?? 0, DbType.Int32);
                parameter.Add(GetUsersProcedure.Params.Limit, request.Limmit ?? ReportConstants.DEFAULT_LIMIT, DbType.Int32);

                using (var multi = await connection.QueryMultipleAsync(GetUsersProcedure.Name, param: parameter,
                    commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new BaseReportQueryResponse<IEnumerable<IUserModel>>
                    {
                        Data = multi.Read<UserModel>().ToList(),
                        Total = multi.Read<int>().FirstOrDefault(),
                        IsSuccess = true,
                        Code = ResponseCodes.Success
                    };
                }
            }
        }
    }
}
