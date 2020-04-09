using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApiDocumentUploader.Services
{
    public class UploadStreamService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UploadStreamService> _logger;
        private readonly string _targetFilePath;

        public UploadStreamService(
            IMapper mapper, 
            ILogger<UploadStreamService> logger, 
            IConfiguration config)
        {
            _mapper = mapper;
            _logger = logger;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }
        
    }
}