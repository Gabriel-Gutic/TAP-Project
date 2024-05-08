using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
	public class RandomVideoSelector : IVideoSelector
	{
		private readonly IRepository<Video> _videoRepository;
		private readonly Random _random;

		public RandomVideoSelector(IRepository<Video> videoRepository)
		{
			_videoRepository = videoRepository;
			_random = new Random();
		}

		public IEnumerable<VideoDto> SelectForUser(Guid userId, int count)
		{
            return SelectRandom(count);
        }

        public IEnumerable<VideoDto> SelectForUser(string username, int count)
        {
			return SelectRandom(count);
        }

		private IEnumerable<VideoDto> SelectRandom(int count)
		{
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive");
            }

            int _count = Math.Min(count, _videoRepository.Count());
            var videos = _videoRepository.GetAll().ToList();

            List<Video> selectedVideos = new List<Video>();

            do
            {
                Video video;
                do
                {
                    video = videos[_random.Next(videos.Count)];
                }
                while (selectedVideos.Count(v => v.Id == video.Id) > 0);

                selectedVideos.Add(video);
            }
            while (selectedVideos.Count < _count);

            return selectedVideos.Select(v => new VideoDto(
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
    }
}
