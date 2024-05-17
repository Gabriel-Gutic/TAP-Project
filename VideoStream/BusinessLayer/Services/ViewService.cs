using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Logger;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ViewService : IViewService
	{
		private readonly IRepository<View> _viewRepository;
		private readonly IAppLogger _logger;

		public ViewService(IRepository<View> viewRepository, IAppLogger logger)
		{
			_viewRepository = viewRepository;
			_logger = logger;
		}

		public IEnumerable<ViewDto> GetAll()
		{
			return _viewRepository.GetAll()
				.Select(v => new ViewDto(v.Id, v.UserId, v.VideoId, v.CreatedAt));
		}

		public ViewDto? Get(Guid id)
		{
			var view = _viewRepository.GetById(id);
			if (view == null)
			{
				return null;
			}
			return new ViewDto(id, view.UserId, view.VideoId, view.CreatedAt);
		}

		public void Insert(ViewDto viewDto)
		{
			_viewRepository.Add(new View()
			{
				UserId = viewDto.UserId,
				VideoId = viewDto.VideoId,
			});

			_viewRepository.SaveChanges();

			_logger.Info("New item inserted in View Table");
		}

		public void Update(Guid id, ViewDto viewDto)
		{
			View view = _viewRepository.GetById(id);
			if (view == null)
			{
				throw new EntityNotFoundException("View not found");
			}

			view.UserId = viewDto.UserId;
			view.VideoId = viewDto.VideoId;

			_viewRepository.SaveChanges();

			_logger.Info("Item updated in View Table");
		}

		public void Delete(Guid id)
		{
			if (!_viewRepository.Delete(id))
			{
				throw new EntityNotFoundException("View not found");
			}
			_viewRepository.SaveChanges();

			_logger.Info("Item deleted from View Table");
		}

        public int Count(Guid videoId)
        {
            return _viewRepository.Count(v => v.VideoId == videoId);
        }
    }
}
