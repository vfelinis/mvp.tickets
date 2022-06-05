using mvp.tickets.data.Models;
using mvp.tickets.domain.Models;


namespace mvp.tickets.data.Procedures
{
    [Procedure]
    public static class GetCategoriesProcedure
    {
        public static string Name => "procGetCategories";
        public static int Version => 1;
        public static class Params
        {
            public static string Id => "@id";
            public static string OnlyActive => "@onlyActive";
            public static string OnlyRoot => "@onlyRoot";
        }

        public static string Text => $@"
/* version={Version} */
CREATE PROCEDURE [{Name}]
    {Params.Id} INT = NULL,
    {Params.OnlyActive} BIT,
    {Params.OnlyRoot} BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        [{nameof(TicketCategory.Id)}] AS [{nameof(CategoryModel.Id)}]
        ,[{nameof(TicketCategory.Name)}] AS [{nameof(CategoryModel.Name)}]
        ,[{nameof(TicketCategory.IsActive)}] AS [{nameof(CategoryModel.IsActive)}]
        ,[{nameof(TicketCategory.IsRoot)}] AS [{nameof(CategoryModel.IsRoot)}]
        ,[{nameof(TicketCategory.DateCreated)}] AS [{nameof(CategoryModel.DateCreated)}]
        ,[{nameof(TicketCategory.DateModified)}] AS [{nameof(CategoryModel.DateModified)}]
        ,[{nameof(TicketCategory.ParentCategoryId)}] AS [{nameof(CategoryModel.ParentCategoryId)}]
    FROM [{TicketCategoryExtension.TableName}]
    WHERE ({Params.Id} IS NULL OR [{nameof(TicketCategory.Id)}] = {Params.Id})
    AND ({Params.OnlyActive} = 0 OR [{nameof(TicketCategory.IsActive)}] = 1)
    AND ({Params.OnlyRoot} = 0 OR [{nameof(TicketCategory.IsRoot)}] = 1)
END";
    }
}