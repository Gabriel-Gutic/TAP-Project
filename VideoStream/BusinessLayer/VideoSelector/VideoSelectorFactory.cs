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
    public class VideoSelectorFactory : IVideoSelectorFactory
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<VideoCategory> _videoCategoryRepository;
        private readonly IRepository<View> _viewRepository;

        public VideoSelectorFactory(IRandomGenerator randomGenerator, IRepository<Video> videoRepository, IRepository<VideoCategory> videoCategoryRepository, IRepository<View> viewRepository)
        {
            _randomGenerator = randomGenerator;
            _videoRepository = videoRepository;
            _videoCategoryRepository = videoCategoryRepository;
            _viewRepository = viewRepository;
        }

        public IVideoSelector Create(string name)
        {
            switch (name)
            {
                case "Random":
                    return new RandomVideoSelector(_videoRepository, _randomGenerator);
                case "Category":
                    return new CategoryVideoSelector(_videoCategoryRepository, _videoRepository, _viewRepository, _randomGenerator);
            }
            return new UnknownVideoSelector();
        }
    }
}
