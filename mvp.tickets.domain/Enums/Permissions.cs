namespace mvp.tickets.domain.Enums
{
    [Flags]
    public enum Permissions
    {
        User     = 0b000000000,
        Admin    = 0b000000001,
        Employee = 0b000000010
    }
}
