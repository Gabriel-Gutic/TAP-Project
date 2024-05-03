using BusinessLayer.Dto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
	public interface IVideoCategoryService
	{
		public IEnumerable<VideoCategoryDto> GetAll();

		public VideoCategoryDto? Get(Guid id);
		
		public void Insert(VideoCategoryDto videoCategoryDto);
		
		public void Update(Guid id, VideoCategoryDto videoCategoryDto);

		public void Delete(Guid id);
	}
}
