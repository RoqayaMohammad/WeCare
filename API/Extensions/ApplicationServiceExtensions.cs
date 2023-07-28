using API.Helpers;
using API.Services;
using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
             IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddLocalization();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddMvc()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(JsonStringLocalizerFactory));
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ar-EG"),
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
            });


            services.AddScoped<IPhotoService, PhotoService>();
            //services.AddScoped<LogUserActivity>();
            //services.AddSignalR();
            //services.AddSingleton<PresenceTracker>();

            return services;
        }
    }
}
