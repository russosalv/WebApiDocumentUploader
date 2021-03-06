﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiDocumentUploader.Model.DTO;
using WebApiDocumentUploader.Model.DTO.Developer;
using WebApiDocumentUploader.Services;

namespace WebApiDocumentUploader.Controllers.v1
{
    public class MultiPartJsonController : BaseController
    {
        private readonly ILogger<MultiPartJsonController> _logger;
        private readonly MultiPartFormDefaultStreamingService _multiPartFormDefaultStreamingService;

        public MultiPartJsonController(
            ILogger<MultiPartJsonController> logger, 
            MultiPartFormDefaultStreamingService multiPartFormDefaultStreamingService)
        {
            _logger = logger;
            _multiPartFormDefaultStreamingService = multiPartFormDefaultStreamingService;
        }
        
        /// <summary>
        /// Test to receive multipart/form as JSON single File without streaming
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("single")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [ProducesResponseType(typeof(string), 200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MultiPartFormTestSingle(MultiPartTestRequestSingleJson request)
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
        /// Test to receive multipart/form as JSON multiple Files as collection without streaming
        /// </summary>
        /// <remarks>
        /// Working not testable with Swagger, seems that create wrong request
        /// test use CURL or Postman
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("collection_of_file")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MultiPartFormTestMultiple(MultiPartTestRequestMultipleJson request)
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
                    var filePath = Path.Combine(@"C:\Temp", formFile.FileName);
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
        
        
    }
}