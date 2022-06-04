namespace mvp.tickets.domain.Models
{
    public interface IBaseRequest
    {
    }
    public record BaseRequest : IBaseRequest
    {
    }

    public interface IBaseCommandRequest : IBaseRequest { }
    public record BaseCommandRequest : BaseRequest, IBaseCommandRequest { }

    public interface IBaseQueryRequest : IBaseRequest { }
    public record BaseQueryRequest : BaseRequest, IBaseQueryRequest { }

    public interface IBaseReportQueryRequest : IBaseQueryRequest
    {
        int? Offset { get; set; }
        int? Limmit { get; set; }
    }
    public record BaseReportQueryRequest : BaseQueryRequest, IBaseReportQueryRequest
    {
        public int? Offset { get; set; }
        public int? Limmit { get; set; }
    }
}
