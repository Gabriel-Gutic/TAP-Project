namespace WebAPI.File
{
	public interface IFileManager
	{
		public Task<byte[]>? Read(IFormFile file);
	}
}
