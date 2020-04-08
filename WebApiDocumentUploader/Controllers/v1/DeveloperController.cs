using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiDocumentUploader.Model.DTO.Developer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiDocumentUploader.Services;

namespace WebApiDocumentUploader.Controllers.v1
{
    public class DeveloperController : BaseController
    {
        private readonly ILogger<DeveloperController> _logger;
        private readonly DeveloperService _developerService;

        public DeveloperController(
            ILogger<DeveloperController> logger,
            DeveloperService developerService)
        {
            _logger = logger;
            _developerService = developerService;
        }

        /// <summary>
        /// Test Echo API
        /// </summary>
        /// <remarks>
        /// Use section here to give additional info to working of API
        /// it's also possible add code as below
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "id": 1,
        ///        "message": "Hi I am a message",
        ///     }
        ///(do not copy, contain invalid JSON char, to test use Swagger editor instead)
        /// </remarks>
        /// <param name="request">EchoRequest</param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns a response DTO (EchoResponse)</response>
        /// <response code="400">If request is not valid</response>            
        [HttpPost("echo")]
        [ProducesResponseType(typeof(EchoResponse), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(EchoRequest request)
        {
            _logger.LogDebug("received request in controller");
            var data = await _developerService.Echo(request);
            return ApiResponseWithData(data);
        }

    }

}