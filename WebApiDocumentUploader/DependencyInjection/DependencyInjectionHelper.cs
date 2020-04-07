using AutoMapper;
using WebApiDocumentUploader.Model.DTO.Developer;
using WebApiDocumentUploader.Services.Developer;
using Microsoft.Extensions.DependencyInjection;

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
        ;
    }
    
    
}