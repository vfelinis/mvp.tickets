using Microsoft.AspNetCore.Localization;
using mvp.tickets.data.Helpers;
using mvp.tickets.web.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterDependencies(builder.Configuration);

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
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapFallbackToFile("index.html");

InitializationHelper.Initialize(app.Services);

if (app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT")}");
}
