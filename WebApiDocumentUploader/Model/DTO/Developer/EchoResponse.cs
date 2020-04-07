namespace WebApiDocumentUploader.Model.DTO.Developer
{
    public class EchoResponse : EchoRequest
    {
        public bool ImResponse { get; set; } = true;
        public int requestMessageLen { get; set; }
    }
}