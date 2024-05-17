using BlazorClient.Contracts;
using Blazorise;
using System.Text;
using System.Text.Json;

namespace BlazorClient.Data
{
    public class VideoUploadData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public FileUploadData VideoData {  get; set; }
        public FileUploadData ImageData { get; set; }

        public VideoUploadData()
        {
            IsPublic = true;
        }
    }
}
