
using BlazorClient.Data;
using System.Text.Json;
using System.Text;

namespace BlazorClient.MultipartAdapter
{
    public class VideoUploadAdapter : IMultipartAdapter
    {
        public MultipartFormDataContent Adapt(object data)
        {
            if (data is not VideoUploadData)
            {
                throw new ArgumentException("Argument must be a VideoUploadData");
            }

            var video = (VideoUploadData)data;

            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");

            if (video.VideoData != null)
            {
                var file = new StreamContent(video.VideoData.Stream);
                file.Headers.ContentType = video.VideoData.MediaType;
                content.Add(file, "Video", video.VideoData.Name);
            }

            if (video.ImageData != null)
            {
                var file = new StreamContent(video.ImageData.Stream);
                file.Headers.ContentType = video.ImageData.MediaType;
                content.Add(file, "Image", video.ImageData.Name);
            }

            content.Add(new StringContent(video.Title, Encoding.UTF8, "application/json"), "Title");
            content.Add(new StringContent(video.Description, Encoding.UTF8, "application/json"), "Description");
            content.Add(new StringContent(JsonSerializer.Serialize(video.IsPublic), Encoding.UTF8, "application/json"), "IsPublic");
            content.Add(new StringContent(video.UserId.ToString(), Encoding.UTF8, "application/json"), "UserId");
            content.Add(new StringContent(video.CategoryId.ToString(), Encoding.UTF8, "application/json"), "CategoryId");
            return content;
        }
    }
}
