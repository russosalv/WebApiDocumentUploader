using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebApiDocumentUploader.Model.DTO.Developer
{
    public class UploadStreamDto
    {
        // public string Name { get; set; }
        // public string Description { get; set; }
        public int Id { get; set; }
        public List<MetaDataDto> MetaDatas { get; set; } 
        public List<IFormFile> Files { get; set; }
    }

    public class UploadStreamClass {
        // public string Name { get; set; }
        // public string Description { get; set; }
        public int Id { get; set; }
        public List<MetaDataDto> MetaDatas { get; set; } 
    }
    
}