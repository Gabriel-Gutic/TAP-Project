
namespace WebAPI.File
{
	public class FileManager : IFileManager
	{
		public async Task<byte[]>? Read(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					await file.CopyToAsync(memoryStream);
					return memoryStream.ToArray();
				}
			}
			
			return null;
		}
	}
}
