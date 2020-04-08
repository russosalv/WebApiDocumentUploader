using System;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace WebApiDocumentUploader.Services
{
    [Obsolete("No big improvements then default serialization and need custom form request")]
    public class MultiPartFormCustomSteamingService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MultiPartFormCustomSteamingService> _logger;

        public MultiPartFormCustomSteamingService(
            IMapper mapper, 
            ILogger<MultiPartFormCustomSteamingService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
    }
}