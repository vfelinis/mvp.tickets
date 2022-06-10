using Microsoft.AspNetCore.Localization;
using mvp.tickets.data.Helpers;
using mvp.tickets.domain.Constants;
using mvp.tickets.web.Extensions;
using mvp.tickets.web.Middlewares;
using System.Globalization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies(builder.Configuration, builder.Environment);

var app = builder.Build();

var supportedCultures = new[]
{
    new CultureInfo("en-US")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    // Formatting numbers, dates, etc.
    SupportedCultures = supportedCultures,
    // UI strings that we have localized.
    SupportedUICultures = supportedCultures
});

app.UseForwardedHeaders();
app.UseHttpsRedirection();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value.TrimStart('/').ToLower();
    if (path.StartsWith(TicketConstants.AttachmentFolder))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        var userId = int.Parse(context.User.Claims.First(s => s.Type == ClaimTypes.Sid).Value);
        if (!context.User.Claims.Any(s => s.Type == AuthConstants.EmployeeClaim)
            || !path.StartsWith($"{TicketConstants.AttachmentFolder}/{userId}/"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

    }
    await next.Invoke();
});
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();

app.MapControllers();
app.MapFallbackToFile("/index.html");

InitializationHelper.Initialize(app.Services);

if (app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT")}");
}
