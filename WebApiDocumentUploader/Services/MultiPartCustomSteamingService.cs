using AutoMapper;
using Microsoft.Extensions.Logging;

namespace WebApiDocumentUploader.Services
{
    public class MultiPartCustomSteamingService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MultiPartCustomSteamingService> _logger;

        public MultiPartCustomSteamingService(
            IMapper mapper, 
            ILogger<MultiPartCustomSteamingService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}