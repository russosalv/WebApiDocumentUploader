using System;
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
    public class SimpleJsonController : BaseController
    {
        private readonly ILogger<SimpleJsonController> _logger;
        private readonly SimpleJsonService _simpleJsonService;

        public SimpleJsonController(ILogger<SimpleJsonController> logger, 
            SimpleJsonService simpleJsonService)
        {
            _logger = logger;
            _simpleJsonService = simpleJsonService;
        }

        [HttpPost("collection_of_file")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> MultiPartFormTestMultiple([FromBody] SimpleJsonRequest request)
        {
            try
            {
                var data = await _simpleJsonService.SaveFile(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
           
        }
    }
}