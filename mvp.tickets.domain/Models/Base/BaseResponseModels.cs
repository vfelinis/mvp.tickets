using mvp.tickets.domain.Enums;

namespace mvp.tickets.domain.Models
{
    public interface IBaseResponse
    {
        bool IsSuccess { get; set; }
        ResponseCodes Code { get; set; }
        string ErrorMessage { get; set; }
    }
    public record BaseResponse : IBaseResponse
    {
        public bool IsSuccess { get; set; }
        public ResponseCodes Code { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface IBaseCommandResponse : IBaseResponse { }
    public record BaseCommandResponse : BaseResponse, IBaseCommandResponse { }

    public interface IBaseQueryResponse : IBaseResponse { }
    public record BaseQueryResponse : BaseResponse, IBaseQueryResponse { }

    public interface IBaseReportQueryResponse<T> : IBaseQueryResponse
    {
        IEnumerable<T> Data { get; set; }
        int Total { get; set; }
    }
    public record BaseReportQueryResponse<T> : BaseQueryResponse, IBaseReportQueryResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
    }
}
