namespace WebAPI.Dto
{
	public class VideoCategoryDtoInput
	{
		public string Name { get; set; }

		public VideoCategoryDtoInput(string name)
		{
			Name = name;
		}
	}
}
