using BlazorClient.Contracts;
using BlazorClient.Data;
using BlazorClient.Dto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class VideoService : IVideoService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpService _httpService;
        private readonly IUserService _userService;

        public VideoService(IHttpService httpService, IUserService userService, NavigationManager navigationManager)
        {
            _httpService = httpService;
            _userService = userService;
            _navigationManager = navigationManager;
        }

        public async Task<VideoData?> GetVideo(string stringId)
        {
            try
            {
                Guid id = new Guid(stringId);

                VideoDto? videoDto = await _httpService.Get<VideoDto>("api/Video/Get", "id", id);

                if (videoDto == null)
                {
                    _navigationManager.NavigateTo("/", true);
                }

                string apiUrl = _httpService.GetAPI();
                VideoData video = new VideoData()
                {
                    Id = id,
                    Title = videoDto.Title,
                    Description = videoDto.Description,
                    ImagePath = Path.Combine(apiUrl, videoDto.ImagePath),
                    Path = Path.Combine(apiUrl, videoDto.Path),
                    IsPublic = videoDto.IsPublic,
                    CreatedAt = videoDto.CreatedAt,
                };

                return video;
            }
            catch
            {
                _navigationManager.NavigateTo("/", true);
            }

            return default;
        }

        public async Task<IEnumerable<VideoCardData>?> SelectForUser()
        {
            UserData? user = await _userService.GetCurrent();

            IList<VideoCardData> videoCards = new List<VideoCardData>();
            try
            {
                var videos = await _httpService.Get<IEnumerable<VideoDto>>("api/VideoSelector/SelectForUsername", "username", user == null ? null : user.Username);

                string apiUrl = _httpService.GetAPI();
                foreach (VideoDto video in videos)
                {
                    UserDto? userDto = await _httpService.Get<UserDto?>("api/User/Get", "id", video.UserId);

                    if (userDto != null)
                    {
                        videoCards.Add(new VideoCardData(
                            video.Id,
                            video.Title,
                            Path.Combine(apiUrl, video.ImagePath),
                            userDto.Username
                        ));
                    }
                }
                
                return videoCards;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
