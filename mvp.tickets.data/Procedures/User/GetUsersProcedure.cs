using mvp.tickets.data.Models;
using mvp.tickets.domain.Models;


namespace mvp.tickets.data.Procedures
{
    [Procedure]
    public static class GetUsersProcedure
    {
        public static string Name => "procGetUsers";
        public static int Version => 1;
        public static class Params
        {
            public static string Offset => "@offset";
            public static string Limit => "@limit";
        }

        public static string Text => $@"
/* version={Version} */
CREATE PROCEDURE [{Name}]
    {Params.Offset} INT,
    {Params.Limit} INT
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        [{nameof(User.Id)}] AS [{nameof(UserModel.Id)}]
        ,[{nameof(User.Email)}] AS [{nameof(UserModel.Email)}]
        ,[{nameof(User.FirstName)}] AS [{nameof(UserModel.FirstName)}]
        ,[{nameof(User.LastName)}] AS [{nameof(UserModel.LastName)}]
        ,[{nameof(User.Permissions)}] AS [{nameof(UserModel.Permissions)}]
        ,[{nameof(User.IsLocked)}] AS [{nameof(UserModel.IsLocked)}]
        ,[{nameof(User.DateCreated)}] AS [{nameof(UserModel.DateCreated)}]
        ,[{nameof(User.DateModified)}] AS [{nameof(UserModel.DateModified)}]
    FROM [{UserExtension.TableName}]
    ORDER BY [{nameof(User.Id)}]
    OFFSET {Params.Offset} ROWS FETCH NEXT {Params.Limit} ROWS ONLY

    SELECT COUNT(*)
    FROM [{UserExtension.TableName}]
END";
    }
}