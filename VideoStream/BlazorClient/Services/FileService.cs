using BlazorClient.Contracts;
using BlazorClient.Data;

namespace BlazorClient.Services
{
    public class FileService : IFileService
    {
        public MultipartFormDataContent? GetFormFile(FileUploadData? fileData, string name)
        {
            if (fileData == null)
            {
                return null;
            }
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileData.Stream), name, fileData.Name);

            return content;
        }
    }
}
