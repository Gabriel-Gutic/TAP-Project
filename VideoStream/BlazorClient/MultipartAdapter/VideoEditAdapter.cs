
using BlazorClient.Data;
using System.Text.Json;
using System.Text;

namespace BlazorClient.MultipartAdapter
{
    public class VideoEditAdapter : IMultipartAdapter
    {
        public MultipartFormDataContent Adapt(object data)
        {
            if (data is not VideoEditData)
            {
                throw new ArgumentException("Argument must be a VideoEditData");
            }

            var video = (VideoEditData)data;

            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");

            if (video.ImageData != null)
            {
                var file = new StreamContent(video.ImageData.Stream);
                file.Headers.ContentType = video.ImageData.MediaType;
                content.Add(file, "Image", video.ImageData.Name);
            }

            content.Add(new StringContent(video.Id.ToString(), Encoding.UTF8, "application/json"), "Id");
            content.Add(new StringContent(video.Title, Encoding.UTF8, "application/json"), "Title");
            content.Add(new StringContent(video.Description, Encoding.UTF8, "application/json"), "Description");
            content.Add(new StringContent(JsonSerializer.Serialize(video.IsPublic), Encoding.UTF8, "application/json"), "IsPublic");
            content.Add(new StringContent(video.CategoryId.ToString(), Encoding.UTF8, "application/json"), "CategoryId");
            return content;
        }
    }
}
