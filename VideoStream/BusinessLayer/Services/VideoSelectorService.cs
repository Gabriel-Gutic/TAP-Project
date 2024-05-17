using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.VideoSelector;
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
	public class VideoSelectorService : IVideoSelectorService
	{
        private readonly IVideoSelectorFactory _videoSelectorFactory;
        private IVideoSelector? _videoSelector;

        private readonly IUserService _userService;

		public VideoSelectorService(IUserService userService, IVideoSelectorFactory videoSelectorFactory)
		{
            _videoSelector = null;
            _userService = userService;
            _videoSelectorFactory = videoSelectorFactory;
		}

		public IEnumerable<VideoDto> SelectForUser(Guid userId, int count)
		{
            var user = _userService.Get(userId);

            CreateVideoSelector(user);

            return _videoSelector.Select(user, count);
        }

        public IEnumerable<VideoDto> SelectForUser(string username, int count)
        {
            var user = _userService.Get(username);

            CreateVideoSelector(user);

            return _videoSelector.Select(user, count);
        }

        private void CreateVideoSelector(UserDto? user)
        {
            if (user == null)
            {
                _videoSelector = _videoSelectorFactory.Create("Random");
            }
            else
            {
                _videoSelector = _videoSelectorFactory.Create("Category");
            }
        }
    }
}
