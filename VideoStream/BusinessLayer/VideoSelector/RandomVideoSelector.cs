using BusinessLayer.Dto;
using BusinessLayer.RandomGenerator;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.VideoSelector
{
    public class RandomVideoSelector : IVideoSelector
    {
        private readonly IRepository<Video> _videoRepository;
        private readonly IRandomGenerator _random;

        public RandomVideoSelector(IRepository<Video> videoRepository, IRandomGenerator random) 
        { 
            _videoRepository = videoRepository;
            _random = random;
        }

        public IEnumerable<VideoDto> Select(UserDto user, int count)
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
                    video = videos[_random.GetInt(videos.Count)];
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
