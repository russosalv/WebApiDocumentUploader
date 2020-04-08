using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApiDocumentUploader.Model.DTO;
using WebApiDocumentUploader.Model.DTO.Developer;

namespace WebApiDocumentUploader.Services
{
    public class MultiPartFormDefaultStreamingService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MultiPartFormDefaultStreamingService> _logger;
        private readonly string _targetFilePath;

        public MultiPartFormDefaultStreamingService(
            IMapper mapper, 
            ILogger<MultiPartFormDefaultStreamingService> logger,
            IConfiguration config)
        {
            _mapper = mapper;
            _logger = logger;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }
        
        public async Task<BaseDeveloperResponse> SaveMultiPartNoStreaming(MultiPartTestRequestSingle request, BaseDeveloperResponse baseDeveloperResponse = null)
        {
            var returner = baseDeveloperResponse ?? new BaseDeveloperResponse();
            _logger.LogDebug($"Execution started");
            _logger.LogDebug($"request.Id : {request.Id}");
            _logger.LogDebug(
                $"request.MetaDatas : {string.Join(',', request.MetaDatas.Select(x => $"[{x.Key} : {x.Value}]"))}");

            if (request.FormFile.Length > 0)
            {
                var filePath = Path.Combine(_targetFilePath, request.FormFile.FileName);
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.FormFile.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }
                
                return returner.Stop($"Document created at {filePath}");
            }

            return returner.Stop();
        }
    }
}