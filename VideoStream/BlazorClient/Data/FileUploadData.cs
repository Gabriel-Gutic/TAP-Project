using System.Net.Http.Headers;

namespace BlazorClient.Data
{
    public class FileUploadData
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
        public MediaTypeHeaderValue? MediaType { get; set; }
    }
}
