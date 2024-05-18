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
					v.Id,
					v.Title,
					v.Description,
					v.ImagePath,
					v.Path,
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
				video.Id,
				video.Title,
				video.Description,
				video.ImagePath,
				video.Path,
				video.IsPublic,
				video.CreatedAt,
				video.UserId,
				video.CategoryId
			);
		}

		public void Insert(VideoDto videoDto)
		{
			_videoRepository.Add(new Video()
			{
				Title = videoDto.Title,
				Description = videoDto.Description,
                ImagePath = videoDto.ImagePath,
                Path = videoDto.Path,
				IsPublic = videoDto.IsPublic,
				UserId = videoDto.UserId,
				CategoryId = videoDto.CategoryId
			});

			_videoRepository.SaveChanges();

			_logger.Info("New item inserted in Video Table");
		}

		public void Update(Guid id, VideoDto videoDto)
		{
			Video video = _videoRepository.GetById(id);
			if (video == null)
			{
				throw new EntityNotFoundException("Video not found");
			}

			video.Title = videoDto.Title;
			video.Description = videoDto.Description;
			video.ImagePath = videoDto.ImagePath;
			video.Path = videoDto.Path;
			video.IsPublic = videoDto.IsPublic;
			video.UserId = videoDto.UserId;
			video.CategoryId = videoDto.CategoryId;

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

		public IEnumerable<VideoDto>? GetAllForUser(Guid userId)
		{
			return _videoRepository.Find(v => v.UserId == userId)
				.OrderByDescending(v => v.CreatedAt)
				.Select(v => new VideoDto(
					v.Id,
					v.Title,
					v.Description,
					v.ImagePath,
					v.Path,
					v.IsPublic,
					v.CreatedAt,
					v.UserId,
					v.CategoryId
				));
		}

        public void Edit(Guid id, EditVideoDto videoDto)
        {
            Video video = _videoRepository.GetById(id);
            if (video == null)
            {
                throw new EntityNotFoundException("Video not found");
            }

            video.Title = videoDto.Title;
            video.Description = videoDto.Description;
			if (videoDto.ImagePath != null)
			{
				video.ImagePath = videoDto.ImagePath;
			}
            video.IsPublic = videoDto.IsPublic;
            video.CategoryId = videoDto.CategoryId;

            _videoRepository.SaveChanges();

            _logger.Info("Item edited in Video Table");
        }
    }
}
