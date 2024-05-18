using BlazorClient.Contracts;
using Blazorise;
using System.Text;
using System.Text.Json;

namespace BlazorClient.Data
{
    public class VideoEditData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid CategoryId { get; set; }
        public string OldImagePath { get; set; }

        public FileUploadData? ImageData { get; set; }
    }
}
