using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EShop.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void ApplyCookieConfigurations(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(option =>
            {
                //option.Cookie.Name = "guest_cart";
                option.ExpireTimeSpan = TimeSpan.FromDays(7);
                option.LoginPath = "/Login";
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.SlidingExpiration = true;
            });
        }
    }
}
