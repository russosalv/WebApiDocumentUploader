using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDocumentUploader.Helper;

namespace WebApiDocumentUploader.Model.DTO
{
    public class MultiPartTestRequestBase
    {
        public int Id { get; set; }

        public List<MetaDataDto> MetaDatas { get; set; } = new List<MetaDataDto>();
    }

    public class MultiPartTestRequestSingle : MultiPartTestRequestBase
    {
        public IFormFile FormFile { get; set; }
    }

    public class MultiPartTestRequestMultiple : MultiPartTestRequestBase
    {
        public IEnumerable<IFormFile> FormFiles { get; set; }
    }
    
    public class MultiPartTestRequestMultiple2 : MultiPartTestRequestBase
    {
        public IFormFile[] Files { get; set; }
    }
    

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class MultiPartTestRequestSingleJson : MultiPartTestRequestSingle
    {
    }
    
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class MultiPartTestRequestMultipleJson : MultiPartTestRequestMultiple
    {
        
    }
}