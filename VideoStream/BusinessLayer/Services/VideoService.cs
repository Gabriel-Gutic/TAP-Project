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
	public class VideoService : IVideoService
	{
		private readonly IRepository<Video> _videoRepository;
		private readonly IAppLogger _logger;

		public VideoService(IRepository<Video> videoRepository, IAppLogger logger)
		{
			_videoRepository = videoRepository;
			_logger = logger;
		}

		public int GetCount()
		{
			return _videoRepository.Count();
		}

		public IEnumerable<VideoDto> GetAll()
		{
			return _videoRepository.GetAll()
				.Select(v => new VideoDto(
						v.Title,
						v.Description,
						v.Image,
						v.Data,
						v.IsPublic,
						v.CreatedAt,
						v.UserId,
						v.CategoryId
					));
		}

        public VideoDto? Get(Guid id)
		{
			var video = _videoRepository.GetById(id);
			if (video == null)
			{
				return null;
			}

			return new VideoDto(
					video.Title,
					video.Description,
					video.Image,
					video.Data,
					video.IsPublic,
					video.CreatedAt,
					video.UserId,
					video.CategoryId
				);
		}

		public void Insert(VideoDto userDto)
		{
			_videoRepository.Add(new Video()
			{
				Title = userDto.Title,
				Description = userDto.Description,
				Image = userDto.Image,
				Data = userDto.Data,
				IsPublic = userDto.IsPublic,
				UserId = userDto.UserId,
				CategoryId = userDto.CategoryId
			});

			_videoRepository.SaveChanges();

			_logger.Info("New item inserted in Video Table");
		}

		public void Update(Guid id, VideoDto userDto)
		{
			Video video = _videoRepository.GetById(id);
			if (video == null)
			{
				throw new EntityNotFoundException("Video not found");
			}

			video.Title = userDto.Title;
			video.Description = userDto.Description;
			video.Image = userDto.Image;
			video.Data = userDto.Data;
			video.IsPublic = userDto.IsPublic;
			video.UserId = userDto.UserId;
			video.CategoryId = userDto.CategoryId;

			_videoRepository.SaveChanges();

			_logger.Info("Item updated in Video Table");
		}

		public void Delete(Guid id)
		{
			if (!_videoRepository.Delete(id))
			{
				throw new EntityNotFoundException("Video not found");
			}
			_videoRepository.SaveChanges();

			_logger.Info("Item deleted from Video Table");
		}
    }
}
