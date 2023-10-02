using AuthJWT.Services.UserService;

namespace AuthJWT.Config.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
