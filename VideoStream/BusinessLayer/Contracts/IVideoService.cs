using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IVideoService
	{
		public IEnumerable<VideoDto> GetAll();

		public VideoDto? Get(Guid id);

		public void Insert(VideoDto userDto);

		public void Update(Guid id, VideoDto userDto);

		public void Delete(Guid id);
	}
}
