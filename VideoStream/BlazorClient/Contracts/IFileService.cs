using BlazorClient.Data;

namespace BlazorClient.Contracts
{
    public interface IFileService
    {
        public MultipartFormDataContent? GetFormFile(FileUploadData? fileData, string name);
    }
}
