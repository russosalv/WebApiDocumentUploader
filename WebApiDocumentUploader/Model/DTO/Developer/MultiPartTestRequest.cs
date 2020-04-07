using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebApiDocumentUploader.Model.DTO.Developer
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

    public class MultiPartTestRequestMore : MultiPartTestRequestBase
    {
        public List<IFormFile> FormFiles { get; set; }
    }
}