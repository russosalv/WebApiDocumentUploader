using AutoMapper;

namespace WebApiDocumentUploader.Model.DTO.Developer
{
    /// <summary>
    /// Class in charge to contains all mapping between
    /// DTOs, Entity to DTOs and other
    /// </summary>
    public class DeveloperMapper : Profile
    {
        /// <summary>
        /// here must insert all maps declarations
        /// additional info to:
        /// http://docs.automapper.org/en/stable/index.html
        /// </summary>
        public DeveloperMapper()
        {
            //EchoRequest to EchoResponse
            CreateMap<EchoRequest, EchoResponse>()
                .ForMember(x => x.requestMessageLen,
                    opt => opt.MapFrom(x => x.Message.Length));
        }
    }
}