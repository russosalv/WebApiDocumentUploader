using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebApiDocumentUploader.Model.DTO;
using WebApiDocumentUploader.Model.DTO.Developer;

namespace WebApiDocumentUploader.Services
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