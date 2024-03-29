using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvp.tickets.data;
using mvp.tickets.data.Models;
using mvp.tickets.domain.Constants;
using mvp.tickets.domain.Enums;
using mvp.tickets.domain.Extensions;
using mvp.tickets.domain.Models;

namespace mvp.tickets.web.Controllers
{
    [ApiController]
    [Route("api/statuses")]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StatusController> _logger;

        public StatusController(ApplicationDbContext dbContext, ILogger<StatusController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IBaseQueryResponse<IEnumerable<IStatusModel>>> Query([FromQuery] StatusQueryRequest request)
        {
            IBaseQueryResponse<IEnumerable<IStatusModel>> response = default;
            try
            {
                var queryable = _dbContext.TicketStatuses.AsNoTracking().AsQueryable();
                if (request?.Id > 0)
                {
                    queryable = queryable.Where(x => x.Id == request.Id);
                }
                if (request?.OnlyActive == true)
                {
                    queryable = queryable.Where(x => x.IsActive == true);
                }
                var entries = await queryable.Select(s => new StatusModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsDefault = s.IsDefault,
                    IsCompletion = s.IsCompletion,
                    IsActive = s.IsActive,
                    DateCreated = s.DateCreated,
                    DateModified = s.DateModified
                }).ToListAsync();

                response = new BaseQueryResponse<IEnumerable<IStatusModel>>
                {
                    IsSuccess = true,
                    Code = ResponseCodes.Success,
                    Data = entries
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseQueryResponse<IEnumerable<IStatusModel>>();
                response.HandleException(ex);
            }

            return response;
        }

        [Authorize(Policy = AuthConstants.AdminPolicy)]
        [HttpPost]
        public async Task<IBaseCommandResponse<int>> Create([FromBody] StatusCreateCommandRequest request)
        {
            if (request == null)
            {
                return new BaseCommandResponse<int>
                {
                    IsSuccess = false,
                    Code = ResponseCodes.BadRequest
                };
            }

            IBaseCommandResponse<int> response = default;

            try
            {
                if (await _dbContext.TicketStatuses.AnyAsync(s => s.Name == request.Name).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<int>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"������ � ��������� {request.Name} ��� ����������."
                    };
                }

                if (request.IsDefault && await _dbContext.TicketStatuses.AnyAsync(s => s.IsDefault).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<int>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"��������� ������ ��� ����������."
                    };
                }

                var entry = new TicketStatus
                {
                    Name = request.Name,
                    IsDefault = request.IsDefault,
                    IsCompletion = request.IsCompletion,
                    IsActive = request.IsActive,
                    DateCreated = DateTimeOffset.Now,
                    DateModified = DateTimeOffset.Now,
                };
                await _dbContext.TicketStatuses.AddAsync(entry).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                response = new BaseCommandResponse<int>
                {
                    IsSuccess = true,
                    Code = ResponseCodes.Success,
                    Data = entry.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseCommandResponse<int>();
                response.HandleException(ex);
            }
            return response;
        }

        [Authorize(Policy = AuthConstants.AdminPolicy)]
        [HttpPut]
        public async Task<IBaseCommandResponse<bool>> Update([FromBody] StatusUpdateCommandRequest request)
        {
            if (request == null)
            {
                return new BaseCommandResponse<bool>
                {
                    IsSuccess = false,
                    Code = ResponseCodes.BadRequest
                };
            }

            IBaseCommandResponse<bool> response = default;

            try
            {
                if (await _dbContext.TicketStatuses.AnyAsync(s => s.Name == request.Name && s.Id != request.Id).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"������ � ��������� {request.Name} ��� ����������.",
                        Data = false
                    };
                }

                if (request.IsDefault && await _dbContext.TicketStatuses.AnyAsync(s => s.IsDefault && s.Id != request.Id).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"��������� ������ ��� ����������.",
                        Data = false
                    };
                }

                var entry = await _dbContext.TicketStatuses.FirstOrDefaultAsync(s => s.Id == request.Id).ConfigureAwait(false);
                if (entry == null)
                {
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.NotFound,
                        Data = false
                    };
                }

                entry.Name = request.Name;
                entry.IsDefault = request.IsDefault;
                entry.IsCompletion = request.IsCompletion;
                entry.IsActive = request.IsActive;
                entry.DateModified = DateTimeOffset.Now;

                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                response = new BaseCommandResponse<bool>
                {
                    IsSuccess = true,
                    Code = ResponseCodes.Success,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseCommandResponse<bool>();
                response.HandleException(ex);
            }
            return response;
        }
    }
}