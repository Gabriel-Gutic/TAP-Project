using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace BlazorClient.Data
{
    public class FileUploadData
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
        public MediaTypeHeaderValue? MediaType { get; set; }

        public FileUploadData() 
        {
            
        }

        public FileUploadData(IBrowserFile file)
        {
            Name = file.Name;
            Stream = file.OpenReadStream(999_999_999_999_999);
            MediaType = new MediaTypeHeaderValue(file.ContentType);
        }
    }
}
