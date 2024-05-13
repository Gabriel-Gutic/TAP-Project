using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IViewService
	{
		public IEnumerable<ViewDto> GetAll();

		public ViewDto? Get(Guid id);

		public int Count(Guid videoId);

		public void Insert(ViewDto viewDto);

		public void Update(Guid id, ViewDto viewDto);

		public void Delete(Guid id);
	}
}
