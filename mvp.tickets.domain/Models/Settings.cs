namespace mvp.tickets.domain.Models
{
    public interface ISettings
    {
        string FirebaseAdminConfig { get; set; }
    }
    public record Settings: ISettings
    {
        public string FirebaseAdminConfig { get; set; }
    }
}
