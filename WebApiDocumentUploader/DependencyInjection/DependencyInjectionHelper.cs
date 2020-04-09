using AutoMapper;
using WebApiDocumentUploader.Model.DTO.Developer;
using Microsoft.Extensions.DependencyInjection;
using WebApiDocumentUploader.Services;

namespace WebApiDocumentUploader.DependencyInjection
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddAllAutoMappers(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(DeveloperMapper))
        ;
        
        public static IServiceCollection AddAllServices(this IServiceCollection services)
            => services
                .AddTransient<DeveloperService>()
                .AddTransient<MultiPartFormDefaultStreamingService>()
                .AddTransient<MultiPartFormCustomSteamingService>()
                .AddTransient<SimpleJsonService>()
                .AddTransient<UploadStreamService>()
        ;
    }
    
    
}