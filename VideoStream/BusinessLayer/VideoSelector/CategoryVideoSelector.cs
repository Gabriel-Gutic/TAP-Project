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
    public class CategoryVideoSelector : IVideoSelector
    {
        private readonly IRepository<VideoCategory> _videoCategoryRepository;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<View> _viewRepository;

        public CategoryVideoSelector(IRepository<VideoCategory> videoCategoryRepository, IRepository<Video> videoRepository, IRepository<View> viewRepository)
        {
            _videoCategoryRepository = videoCategoryRepository;
            _videoRepository = videoRepository;
            _viewRepository = viewRepository;
        }

        public IEnumerable<VideoDto> Select(UserDto user, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive");
            }

            int _count = Math.Min(count, _videoRepository.Count(v => v.IsPublic));

            List<Video> selectedVideos = new List<Video>();

            var categories =
                (from category in _videoCategoryRepository.GetAll()
                select new
                { 
                    Id = category.Id,
                    Views = 
                        (from view in _viewRepository.GetAll()
                         where view.UserId == user.Id
                         join video in _videoRepository.GetAll() on new { CategoryId = category.Id, ViewVideoId = view.VideoId } equals new { CategoryId = video.CategoryId, ViewVideoId = video.Id }
                         select new {}
                         ).Count(),
                }).OrderByDescending(el => el.Views);

            foreach (var category in categories)
            {
                var videos = _videoRepository.Find(v => v.IsPublic && v.CategoryId == category.Id)
                    .OrderByDescending(v => 
                        (from view in _viewRepository.GetAll()
                        where view.VideoId == v.Id
                        select view).Count()
                    );
                if (videos == null)
                    continue;

                foreach (var video in videos)
                {
                    selectedVideos.Add(video);
                    if (selectedVideos.Count >= _count)
                    {
                        break;
                    }
                }

                if (selectedVideos.Count >= _count)
                {
                    break;
                }
            }

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
