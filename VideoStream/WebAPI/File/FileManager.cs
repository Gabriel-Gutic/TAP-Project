
using WebAPI.Exceptions;

namespace WebAPI.File
{
	public class FileManager : IFileManager
	{
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string?> Extract(IFormFile file, string destination)
		{
            try
            {
                if (file == null || file.Length == 0)
                    return null;

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, destination);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine(destination, uniqueFileName);
            }
            catch
            {
                throw new FileUploadException($"Failed to upload file: '{file.Name}'");
            }
        }

        public async Task<string?> ExtractImage(IFormFile file)
        {
            return await Extract(file, "Images");
        }

        public async Task<string?> ExtractVideo(IFormFile file)
        {
            return await Extract(file, "Videos");
        }

        public void Delete(string? filePath)
        {
            if (filePath == null)
                return;
            var path = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else
            {
                throw new FileNotFoundException($"File '{filePath}' not found");
            }
        }
    }
}
