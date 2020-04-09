using System.Collections.Generic;

namespace WebApiDocumentUploader.Model.DTO
{
    public class SimpleJsonRequest
    {
        public int Id { get; set; }

        public List<MetaDataDto> MetaDatas { get; set; } = new List<MetaDataDto>();
        
        public IEnumerable<FileNameAndBase45> ContentFiles { get; set; }
    }

    public class FileNameAndBase45
    {
        public string FileName { get; set; }

        public byte[] ContentFile { get; set; }
    }
}