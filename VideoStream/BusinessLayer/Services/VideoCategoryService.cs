using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public class VideoCategoryService : IVideoCategoryService
	{
		private readonly IRepository<VideoCategory> _videoCategoryRepository;
		private readonly IAppLogger _logger;

		public VideoCategoryService(IRepository<VideoCategory> videoCategoryRepository, IAppLogger logger)  
		{ 
			_videoCategoryRepository = videoCategoryRepository;
			_logger = logger;
		}

		public IEnumerable<VideoCategoryDto> GetAll()
		{
			return _videoCategoryRepository.GetAll()
				.Select(vc => new VideoCategoryDto(vc.Id, vc.Name, vc.CreatedAt));
		}

		public VideoCategoryDto? Get(Guid id)
		{
			var videoCategory = _videoCategoryRepository.GetById(id);
			if (videoCategory == null)
			{
				return null;
			}
			return new VideoCategoryDto(id, videoCategory.Name, videoCategory.CreatedAt);
		}

		public void Insert(VideoCategoryDto videoCategoryDto)
		{
			_videoCategoryRepository.Add(new VideoCategory(videoCategoryDto.Name));

			_videoCategoryRepository.SaveChanges();

			_logger.Info("New item inserted in VideoCategory Table");
		}

		public void Update(Guid id, VideoCategoryDto videoCategoryDto)
		{
			VideoCategory videoCategory = _videoCategoryRepository.GetById(id);
			if (videoCategory == null)
			{
				throw new EntityNotFoundException("Video Category not found");
			}

			videoCategory.Name = videoCategoryDto.Name;

			_videoCategoryRepository.SaveChanges();

			_logger.Info("Item updated in VideoCategory Table");
		}

		public void Delete(Guid id)
		{
			if (!_videoCategoryRepository.Delete(id))
			{
				throw new EntityNotFoundException("Video Category not found");
			}
			_videoCategoryRepository.SaveChanges();

			_logger.Info("Item deleted from VideoCategory Table");
		}
	}
}
