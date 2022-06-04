using mvp.tickets.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvp.tickets.domain.Models
{
    public interface IUserLoginCommandRequest: IBaseCommandRequest
    {
        string IdToken { get; set; }
    }
    public record UserLoginCommandRequest: BaseCommandRequest, IUserLoginCommandRequest
    {
        public string IdToken { get; set; }
    }

    public interface IUserCreateCommandRequest : IBaseCommandRequest
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Permissions Permissions { get; set; }
    }
    public record UserCreateCommandRequest : BaseCommandRequest, IUserCreateCommandRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Permissions Permissions { get; set; }
    }
}
