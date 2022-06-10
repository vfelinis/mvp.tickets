using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace mvp.tickets.domain.Models
{
    public interface ITicketCreateCommandRequest : IBaseCommandRequest
    {
        string Name { get; set; }
        int TicketCategoryId { get; set; }
        string Text { get; set; }
        List<IFormFile> Files { get; set; }
    }
    public record TicketCreateCommandRequest : BaseCommandRequest, ITicketCreateCommandRequest
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
        [Required]
        public int TicketCategoryId { get; set; }
        [StringLength(maximumLength: 2000)]
        public string Text { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public interface ITicketCommentCreateCommandRequest : IBaseCommandRequest
    {
        string Text { get; set; }
        bool IsInternal { get; set; }
        List<IFormFile> Files { get; set; }
    }
    public record TicketCommentCreateCommandRequest : BaseCommandRequest, ITicketCommentCreateCommandRequest
    {
        [StringLength(maximumLength: 2000)]
        public string Text { get; set; }
        public bool IsInternal { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public interface ITicketUpdateCommandRequest : ITicketCreateCommandRequest
    {
        int Id { get; set; }
    }

    public record TicketUpdateCommandRequest : TicketCreateCommandRequest, ITicketUpdateCommandRequest
    {
        public int Id { get; set; }
    }
}
