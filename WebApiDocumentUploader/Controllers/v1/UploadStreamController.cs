using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UploadStream;
using WebApiDocumentUploader.Model.DTO;
using WebApiDocumentUploader.Model.DTO.Developer;
using WebApiDocumentUploader.Services;

namespace WebApiDocumentUploader.Controllers.v1
{
    public class UploadStreamController : BaseController
    {
        private readonly ILogger<UploadStreamController> _logger;
        private readonly UploadStreamService _uploadStreamService;
        
        const int BUF_SIZE = 4096;

        public UploadStreamController(ILogger<UploadStreamController> logger, 
            UploadStreamService uploadStreamService)
        {
            _logger = logger;
            _uploadStreamService = uploadStreamService;
        }
        
        [HttpPost("collection_of_file")]
        [ProducesResponseType(typeof(BaseDeveloperResponse), 200)]
        [DisableRequestSizeLimit]
        [Obsolete]
        public async Task<IActionResult> ControllerModelStreamBindingEnabled(UploadStreamDto model) {
            
            byte[] buffer = new byte[BUF_SIZE];
            List<IFormFile> files = new List<IFormFile>();

            var streamModel = await this.StreamFiles<UploadStreamClass>(async x => {
                using (var stream = x.OpenReadStream())
                    while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0) ;
                files.Add(x);
            });

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new
            {
                //model, 
                Received = streamModel, 
                //Files = files
                Files = files.Select(x => new {
                    x.Name,
                    x.FileName,
                    x.ContentDisposition,
                    x.ContentType,
                    x.Length
                })
            });
        }
        
        [HttpPost("stream")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ControllerModelStream() {
            byte[] buffer = new byte[BUF_SIZE];
            List<IFormFile> files = new List<IFormFile>();

            var model = await this.StreamFiles<StreamModel>(async x => {
                using (var stream = x.OpenReadStream())
                    while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                        ;
                files.Add(x);
            });

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new {
                Model = model,
                Files = files.Select(x => new {
                    x.Name,
                    x.FileName,
                    x.ContentDisposition,
                    x.ContentType,
                    x.Length
                })
            });
        }
        
        [Obsolete]
        [HttpPost("stream_modelDisabled")]
        //[DisableFormModelBinding]
        public async Task<IActionResult> ControllerModelStreamModelDisabled([FromForm]UploadModel uploadModel) {
            byte[] buffer = new byte[BUF_SIZE];
            List<IFormFile> files = new List<IFormFile>();

            var model = await this.StreamFiles<StreamModel>(async x => {
                using (var stream = x.OpenReadStream())
                    while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
                        ;
                files.Add(x);
            });

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new {
                Model = model,
                Files = files.Select(x => new {
                    x.Name,
                    x.FileName,
                    x.ContentDisposition,
                    x.ContentType,
                    x.Length
                })
            });
        }
        
    }
    
    //DEBUG ONLY
    public class UploadModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    //DEBUG ONLY
    class StreamModel {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}