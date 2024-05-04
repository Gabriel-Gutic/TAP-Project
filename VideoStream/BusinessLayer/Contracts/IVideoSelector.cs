using BusinessLayer.Dto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IVideoSelector
	{
		public IEnumerable<FullVideoDto> SelectForUser(Guid userId, int count);
	}
}
