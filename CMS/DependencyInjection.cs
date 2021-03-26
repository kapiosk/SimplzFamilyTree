using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplzFamilyTree.Data;

namespace SimplzFamilyTree.CMS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            
            services.AddHttpContextAccessor();

            services.AddAntiforgery(x => x.HeaderName = configuration["Antiforgery"]);
            services.AddRazorPages();
            //services.AddRazorPages(opts =>
            //{
            //    opts.Conventions.AuthorizeFolder("/");
            //    opts.Conventions.AllowAnonymousToPage("/Authorization/Login");
            //});

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
                    .AddJsonOptions(opt =>
                    {
                        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                        opt.JsonSerializerOptions.AllowTrailingCommas = true;
                        opt.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                    });

            services.Configure<CookiePolicyOptions>(opts =>
            {
                opts.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                opts.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            });

            services.AddDefaultIdentity<ApplicationUser>(opts => configuration.Bind("IdentityCoreSettings", opts))
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
