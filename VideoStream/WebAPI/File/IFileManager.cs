namespace WebAPI.File
{
	public interface IFileManager
	{
        public Task<string?> Extract(IFormFile file, string destination);
        public Task<string?> ExtractImage(IFormFile file);
        public Task<string?> ExtractVideo(IFormFile file);
        public void Delete(string? filePath);
    }
}
