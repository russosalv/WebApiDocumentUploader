using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApiDocumentUploader.Model.DTO;
using WebApiDocumentUploader.Model.DTO.Developer;
using WebApiDocumentUploader.Services;

namespace WebApiDocumentUploader.Controllers.v1
{
    public class MultiPartFormDefaultStreamingController : BaseController
    {
        private readonly ILogger<MultiPartFormDefaultStreamingController> _logger;
        private readonly MultiPartFormDefaultStreamingService _multiPartFormDefaultStreamingService;
        private readonly string _targetFilePath;

        public MultiPartFormDefaultStreamingController(
            ILogger<MultiPartFormDefaultStreamingController> logger, 
            MultiPartFormDefaultStreamingService multiPartFormDefaultStreamingService,
            IConfiguration config)
        {
            _logger = logger;
            _multiPartFormDefaultStreamingService = multiPartFormDefaultStreamingService;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }
        
        
        /// <summary>
        /// Test to receive multipart/form single File without streaming
        /// </summary>
        /// <remarks>
        /// Metadata not testable with swagger, but use postman or similar instead
        /// example of postman request in the project
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("single")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [ProducesResponseType(typeof(string), 200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MultiPartFormTestSingle([FromForm] MultiPartTestRequestSingle request)
        {
            try
            {
                var data = await _multiPartFormDefaultStreamingService.SaveMultiPartNoStreaming(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        

        /// <summary>
        /// Test to receive multipart/form multiple Files as collection without streaming
        /// </summary>
        /// <remarks>
        /// Metadata and MultipleFile not testable with swagger, but use postman or similar instead
        /// example of postman request in the project
        /// swagger issue:https://github.com/OAI/OpenAPI-Specification/issues/254
        /// swagger ui issue: https://github.com/swagger-api/swagger-ui/issues/4600
        /// swagger ui issue: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1029
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("collection_of_file")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MultiPartFormTestMultiple([FromForm] MultiPartTestRequestMultiple request)
        {
            var returner = new BaseDeveloperResponse();
            _logger.LogDebug($"Execution started");
            _logger.LogDebug($"request.Id : {request.Id}");
            _logger.LogDebug(
                $"request.MetaDatas : {string.Join(',', request.MetaDatas.Select(x => $"[{x.Key} : {x.Value}]"))}");

            var returnPaths = new List<string>();
            foreach (var formFile in request.FormFiles)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(_targetFilePath, formFile.FileName);
                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }
                    returnPaths.Add(filePath);
                }
            }
            
            return Ok(returner.Stop($"Document created at: {string.Join(" , ",returnPaths)}"));
           
        }
        
        /// <summary>
        /// Test to receive multipart/form multiple File as Multi-request without streaming
        /// SEEMS IMPOSSIBLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("multi_request")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [DisableRequestSizeLimit]
        [Obsolete]
        public async Task<IActionResult> MultiPartFormTestMultiple([FromForm] List<MultiPartTestRequestSingle> requests)
        {
            try
            {
                var returner = new BaseDeveloperResponse();
                foreach (var request in requests)
                {
                    var data = await _multiPartFormDefaultStreamingService.SaveMultiPartNoStreaming(request, returner);
                }
                
                return Ok(returner);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
           
        }
    }
}