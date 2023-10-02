using AuthJWT.Config.Permissions;
using AuthJWT.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace AuthJWT.Config.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IAuthorizationPolicyProvider,
                AuthorizationPolicyProvider>();

            return services;
        }
    }
}
