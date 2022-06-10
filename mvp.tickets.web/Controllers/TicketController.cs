using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mvp.tickets.data;
using mvp.tickets.data.Models;
using mvp.tickets.data.Procedures;
using mvp.tickets.domain.Constants;
using mvp.tickets.domain.Enums;
using mvp.tickets.domain.Extensions;
using mvp.tickets.domain.Models;
using System.Data;
using System.Security.Claims;
using System.Web;

namespace mvp.tickets.web.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConnectionStrings _connectionStrings;
        private readonly ILogger<TicketController> _logger;
        private readonly IWebHostEnvironment _environment;

        public TicketController(ApplicationDbContext dbContext, IConnectionStrings connectionStrings, ILogger<TicketController> logger, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _connectionStrings = connectionStrings;
            _logger = logger;
            _environment = environment;
        }

        [Authorize]
        [HttpPost("report")]
        public async Task<IBaseReportQueryResponse<IEnumerable<ITicketModel>>> Report(BaseReportQueryRequest request)
        {
            IBaseReportQueryResponse<IEnumerable<ITicketModel>> response = default;
            try
            {
                if (!User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim) && !User.Claims.Any(s => s.Type == AuthConstants.UserClaim))
                {
                    return new BaseReportQueryResponse<IEnumerable<ITicketModel>>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.Unauthorized
                    };
                }

                using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    if (!User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim))
                    {
                        var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
                        parameter.Add(TicketsReportProcedure.Params.SearchByReporterId, userId, DbType.Int32);
                    }
                    if (request.SearchBy?.Any() == true)
                    {
                        foreach (var search in request.SearchBy.Where(s => !string.IsNullOrWhiteSpace($"{s.Value}")))
                        {
                            if (string.Equals(search.Key, nameof(Ticket.Id), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchById, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.IsClosed), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByIsClosed, Convert.ToBoolean(search.Value), DbType.Boolean);
                            }
                            if (User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim) && string.Equals(search.Key, nameof(Ticket.ReporterId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByReporterId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.TicketPriorityId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByTicketPriorityId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.TicketQueueId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByTicketQueueId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.TicketResolutionId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByTicketResolutionId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.TicketStatusId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByTicketStatusId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                            if (string.Equals(search.Key, nameof(Ticket.TicketCategoryId), StringComparison.OrdinalIgnoreCase))
                            {
                                parameter.Add(TicketsReportProcedure.Params.SearchByTicketCategoryId, Convert.ToInt32(search.Value), DbType.Int32);
                            }
                        }
                    }

                    parameter.Add(TicketsReportProcedure.Params.SortBy, request.SortBy, DbType.String);
                    parameter.Add(TicketsReportProcedure.Params.SortDirection, request.SortDirection.ToString(), DbType.String);
                    parameter.Add(TicketsReportProcedure.Params.Offset, request.Offset, DbType.Int32);
                    parameter.Add(TicketsReportProcedure.Params.Limit, ReportConstants.DEFAULT_LIMIT, DbType.Int32);

                    var query = await connection.QueryAsync<TicketReportModel>(TicketsReportProcedure.Name, param: parameter,
                        commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                    var entries = query.ToList();
                    return new BaseReportQueryResponse<IEnumerable<ITicketModel>>
                    {
                        Data = entries,
                        Total = entries.FirstOrDefault()?.Total ?? 0,
                        IsSuccess = true,
                        Code = ResponseCodes.Success
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseReportQueryResponse<IEnumerable<ITicketModel>>();
                response.HandleException(ex);
            }

            return response;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IBaseQueryResponse<ITicketModel>> Get(int id, [FromQuery] TicketQueryRequest request)
        {
            IBaseQueryResponse<ITicketModel> response = default;
            try
            {
                if (!User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim) && !User.Claims.Any(s => s.Type == AuthConstants.UserClaim))
                {
                    return new BaseQueryResponse<ITicketModel>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.Unauthorized
                    };
                }

                using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add(TicketsReportProcedure.Params.SearchById, id, DbType.Int32);
                    parameter.Add(TicketsReportProcedure.Params.SortBy, nameof(Ticket.Id), DbType.String);
                    parameter.Add(TicketsReportProcedure.Params.SortDirection, SortDirection.ASC.ToString(), DbType.String);
                    parameter.Add(TicketsReportProcedure.Params.Offset, 0, DbType.Int32);
                    parameter.Add(TicketsReportProcedure.Params.Limit, 1, DbType.Int32);

                    var entry = await connection.QueryFirstOrDefaultAsync<TicketReportModel>(TicketsReportProcedure.Name, param: parameter,
                        commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                    if (entry == null)
                    {
                        return new BaseQueryResponse<ITicketModel>
                        {
                            IsSuccess = false,
                            Code = ResponseCodes.NotFound
                        };
                    }

                    var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
                    if (!User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim) && entry.ReporterId != userId)
                    {
                        return new BaseQueryResponse<ITicketModel>
                        {
                            IsSuccess = false,
                            Code = ResponseCodes.Unauthorized
                        };
                    }

                    var includeIternal = User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim) && !request.IsUserView;
                    entry.TicketComments = await _dbContext.TicketComments
                        .Where(s => s.TicketId == entry.Id && s.IsActive && (includeIternal || !s.IsInternal))
                        .Select(s => new TicketCommentModel
                        {
                            Id = s.Id,
                            Text = s.Text,
                            IsInternal = s.IsInternal,
                            CreatorId = s.CreatorId,
                            CreatorEmail = s.Creator.Email,
                            CreatorFirstName = s.Creator.FirstName,
                            CreatorLastName = s.Creator.LastName,
                            DateCreated = s.DateCreated,
                            DateModified = s.DateModified,
                            TicketCommentAttachmentModels = s.TicketCommentAttachments.Where(x => x.IsActive).Select(x => new TicketCommentAttachmentModel
                            {
                                Id = x.Id,
                                DateCreated = x.DateCreated,
                                OriginalFileName = x.OriginalFileName,
                                Path = x.FileName + "." + x.Extension
                            }).ToList()
                        })
                        .ToListAsync();

                    foreach(var ticketCommentAttachment in entry.TicketComments.SelectMany(s => s.TicketCommentAttachmentModels))
                    {
                        ticketCommentAttachment.Path = $"/{TicketConstants.AttachmentFolder}/{ticketCommentAttachment.Path}";
                    }

                    return new BaseQueryResponse<ITicketModel>
                    {
                        Data = entry,
                        IsSuccess = true,
                        Code = ResponseCodes.Success
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new BaseQueryResponse<ITicketModel>();
                response.HandleException(ex);
            }

            return response;
        }

        [Authorize(Policy = AuthConstants.UserPolicy)]
        [HttpPost]
        public async Task<IBaseCommandResponse<int>> Create([FromForm] TicketCreateCommandRequest request)
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
                var defaultQueue = await _dbContext.TicketQueues.AsNoTracking().FirstOrDefaultAsync(s => s.IsDefault);
                if (defaultQueue == null)
                {
                    return new BaseCommandResponse<int>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = "¬ системе отсутствует первична€ очередь за€вок."
                    };
                }

                var defaultStatus = await _dbContext.TicketStatuses.AsNoTracking().FirstOrDefaultAsync(s => s.IsDefault);
                if (defaultStatus == null)
                {
                    return new BaseCommandResponse<int>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = "¬ системе отсутствует первичный статус за€вок."
                    };
                }

                var userId = int.Parse(User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
                var entry = new Ticket
                {
                    Name = HttpUtility.HtmlAttributeEncode(request.Name),
                    IsClosed = false,
                    DateCreated = DateTimeOffset.Now,
                    DateModified = DateTimeOffset.Now,
                    ReporterId = userId,
                    TicketQueueId = defaultQueue.Id,
                    TicketStatusId = defaultStatus.Id,
                    TicketCategoryId = request.TicketCategoryId,
                    TicketObservations = new List<TicketObservation>
                    {
                        new TicketObservation
                        {
                            DateCreated = DateTimeOffset.Now,
                            UserId = userId
                        }
                    }
                };
                if (!string.IsNullOrWhiteSpace(request.Text) || request.Files?.Any() == true)
                {
                    var ticketComment = new TicketComment
                    {
                        Ticket = entry,
                        Text = HttpUtility.HtmlAttributeEncode(request.Text),
                        IsInternal = false,
                        IsActive = true,
                        DateCreated = DateTimeOffset.Now,
                        DateModified = DateTimeOffset.Now,
                        CreatorId = userId,
                    };
                    entry.TicketComments.Add(ticketComment);

                    if (request.Files?.Any() == true)
                    {
                        foreach (var file in request.Files)
                        {
                            var ext = Path.GetExtension(file.FileName).Trim('.').ToLower();
                            var ticketCommentAttachment = new TicketCommentAttachment
                            {
                                TicketComment = ticketComment,
                                DateCreated = DateTimeOffset.Now,
                                DateModified = DateTimeOffset.Now,
                                IsActive = true,
                                OriginalFileName = file.FileName,
                                Extension = ext,
                                FileName = Guid.NewGuid().ToString()
                            };
                            ticketComment.TicketCommentAttachments.Add(ticketCommentAttachment);

                            var path = Path.Join(_environment.WebRootPath, $"/{TicketConstants.AttachmentFolder}/{userId}/{ticketCommentAttachment.FileName}.{ext}");
                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                            using (var stream = System.IO.File.Create(path))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                }

                await _dbContext.Tickets.AddAsync(entry).ConfigureAwait(false);
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
        public async Task<IBaseCommandResponse<bool>> Update([FromBody] QueueUpdateCommandRequest request)
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
                if (await _dbContext.TicketQueues.AnyAsync(s => s.Name == request.Name && s.Id != request.Id).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"«апись с названием {request.Name} уже существует.",
                        Data = false
                    };
                }

                if (request.IsDefault && await _dbContext.TicketQueues.AnyAsync(s => s.IsDefault && s.Id != request.Id).ConfigureAwait(false))
                {
                    return new BaseCommandResponse<bool>
                    {
                        IsSuccess = false,
                        Code = ResponseCodes.BadRequest,
                        ErrorMessage = $"ѕервична€ очередь уже существует.",
                        Data = false
                    };
                }

                var entry = await _dbContext.TicketQueues.FirstOrDefaultAsync(s => s.Id == request.Id).ConfigureAwait(false);
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