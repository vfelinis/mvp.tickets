using mvp.tickets.data.Models;
using mvp.tickets.domain.Models;


namespace mvp.tickets.data.Procedures
{
    [Procedure]
    public static class UsersReportProcedure
    {
        public static string Name => "procUsersReport";
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
        [{nameof(User.Id)}] AS [{nameof(UserReportModel.Id)}]
        ,[{nameof(User.Email)}] AS [{nameof(UserReportModel.Email)}]
        ,[{nameof(User.FirstName)}] AS [{nameof(UserReportModel.FirstName)}]
        ,[{nameof(User.LastName)}] AS [{nameof(UserReportModel.LastName)}]
        ,[{nameof(User.Permissions)}] AS [{nameof(UserReportModel.Permissions)}]
        ,[{nameof(User.IsActive)}] AS [{nameof(UserReportModel.IsActive)}]
        ,[{nameof(User.IsLocked)}] AS [{nameof(UserReportModel.IsLocked)}]
        ,[{nameof(User.DateCreated)}] AS [{nameof(UserReportModel.DateCreated)}]
        ,[{nameof(User.DateModified)}] AS [{nameof(UserReportModel.DateModified)}]
    FROM [{UserExtension.TableName}]
    ORDER BY [{nameof(User.Id)}]
    OFFSET {Params.Offset} ROWS FETCH NEXT {Params.Limit} ROWS ONLY

    SELECT COUNT(*)
    FROM [{UserExtension.TableName}]
END";
    }
}