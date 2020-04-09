using System.Collections.Generic;
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
    public class SimpleJsonService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SimpleJsonService> _logger;
        private readonly string _targetFilePath;


        public SimpleJsonService(IMapper mapper,
            ILogger<SimpleJsonService> logger,
            IConfiguration config)
        {
            _mapper = mapper;
            _logger = logger;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

        }

        public async Task<BaseDeveloperResponse> SaveFile(SimpleJsonRequest request,
            BaseDeveloperResponse baseDeveloperResponse = null)
        {
            var returner = baseDeveloperResponse ?? new BaseDeveloperResponse();
            _logger.LogDebug($"Execution started");
            _logger.LogDebug($"request.Id : {request.Id}");
            _logger.LogDebug(
                $"request.MetaDatas : {string.Join(',', request.MetaDatas.Select(x => $"[{x.Key} : {x.Value}]"))}");

            var returnPaths = new List<string>();
            
            foreach (var contentFile in request.ContentFiles)
            {
                if (contentFile.ContentFile.Any())
                {
                    var filePath = Path.Combine(_targetFilePath, contentFile.FileName);
                
                    await using (var memoryStream = new FileStream(filePath, FileMode.Create))
                    {
                        using (MemoryStream ms = new MemoryStream(contentFile.ContentFile))
                        {
                            await ms.CopyToAsync(memoryStream);
                            await memoryStream.FlushAsync();
                        }
                        returnPaths.Add(filePath);
                    }
                }
            }
            
            return returner.Stop($"Document created at {string.Join(" , ", returnPaths)}");
        }
    }
}