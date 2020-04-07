using System.Threading.Tasks;
using AutoMapper;
using WebApiDocumentUploader.Model.DTO.Developer;
using Microsoft.Extensions.Logging;

namespace WebApiDocumentUploader.Services.Developer
{
    public class DeveloperService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeveloperService> _logger;

        public DeveloperService(IMapper mapper, ILogger<DeveloperService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EchoResponse> Echo(EchoRequest request)
        {
            _logger.LogDebug("received request in service");
            return _mapper.Map<EchoResponse>(request);
        }
    }
}