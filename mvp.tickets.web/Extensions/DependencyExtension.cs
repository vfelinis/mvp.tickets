using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using mvp.tickets.data;
using mvp.tickets.data.Stores;
using mvp.tickets.domain.Services;
using mvp.tickets.domain.Stores;

namespace mvp.tickets.web.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            #region Web
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardLimit = 1;
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            #endregion

            #region Data
            var connectionsStrings = new ConnectionStrings
            {
                DefaultConnection = config.GetConnectionString("DefaultConnection")
            };
            services.AddSingleton<IConnectionStrings>(connectionsStrings);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionsStrings.DefaultConnection));
            services.AddTransient<IUserStore, UserStore>();
            #endregion

            #region Domain
            services.AddTransient<IUserService, UserService>();
            #endregion

            var appSettings = config.Get<AppSettings>();
            appSettings.FirebaseAdminConfig = File.ReadAllText(Path.Combine(env.ContentRootPath, "FirebaseAdmin.json"));
            services.AddSingleton(appSettings);
        }
    }
}
