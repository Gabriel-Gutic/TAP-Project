using BusinessLayer.Dto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IVideoSelectorService
	{
		public IEnumerable<VideoDto> SelectForUser(Guid userId, int count);
		public IEnumerable<VideoDto> SelectForUser(string? username, int count);
	}
}
