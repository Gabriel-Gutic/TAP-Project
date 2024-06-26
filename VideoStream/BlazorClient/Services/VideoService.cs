﻿using BlazorClient.Contracts;
using BlazorClient.Data;
using BlazorClient.Dto;
using BlazorClient.Events;
using BlazorClient.MultipartAdapter;
using BlazorClient.Pages.AuthPages;
using Blazorise;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class VideoService : IVideoService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpService _httpService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;
        private readonly IViewService _viewService;
        private readonly ISubscriberService _subscriberService;
        private readonly IMultipartAdapter _multipartUploadAdapter;
        private readonly IMultipartAdapter _multipartEditAdapter;

        private readonly IEventController _eventController; 

        public VideoService(IHttpService httpService, IUserService userService,  NavigationManager navigationManager, IFeedbackService feedbackService, IViewService viewService, ISubscriberService subscriberService, IEventController eventController)
        {
            _httpService = httpService;
            _userService = userService;
            _navigationManager = navigationManager;
            _feedbackService = feedbackService;
            _viewService = viewService;
            _subscriberService = subscriberService;
            _eventController = eventController;

            _multipartUploadAdapter = new VideoUploadAdapter();
            _multipartEditAdapter = new VideoEditAdapter();
        }

		public async Task<VideoData?> GetVideo(string stringId)
        {
            try
            {
                string apiUrl = _httpService.GetAPI();
                Guid id = new Guid(stringId);

                VideoDto? videoDto = await _httpService.Get<VideoDto>("api/Video/Get", "id", id);

                if (videoDto == null)
                {
                    _navigationManager.NavigateTo("/", true);
                }

                UserData? creator = await _userService.Get(videoDto.UserId);

                if (creator == null)
                {
                    _navigationManager.NavigateTo("/", true);
                }

                int subscriberCount = await _subscriberService.Count(creator.Id);

                int likeCount = await _feedbackService.CountPositive(id);
                int dislikeCount = await _feedbackService.CountNegative(id);

                int views = await _viewService.Count(id);


                VideoData video = new VideoData()
                {
                    Id = id,
                    Title = videoDto.Title,
                    Description = videoDto.Description,
                    ImagePath = Path.Combine(apiUrl, videoDto.ImagePath),
                    Path = Path.Combine(apiUrl, videoDto.Path),
                    IsPublic = videoDto.IsPublic,
                    CreatedAt = videoDto.CreatedAt,

                    CreatorId = creator.Id,
                    CreatorUsername = creator.Username,
                    CreatorImagePath = creator.ImagePath,
                    CreatorSubscriberCount = subscriberCount,

                    ViewCount = views,

                    LikeCount = likeCount,
                    DislikeCount = dislikeCount,
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

		public async Task UploadVideo(VideoUploadData data)
		{
            string? errorMessage = null;

            try
            {
                var success = await _httpService.PostMultipart("api/Video/Insert", _multipartUploadAdapter.Adapt(data));
                if (success)
                {
                    if (data.IsPublic)
                    {
                        await _eventController.Invoke("VideoUpload", new { CreatorId = data.UserId });
                    }
                    _navigationManager.NavigateTo($"/", true);
                }
                else
                {
                    throw new Exception("An error occured. Try again.");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            _navigationManager.NavigateTo($"/videoupload/{errorMessage}");
        }

		public async Task<IEnumerable<VideoCardData>?> GetAllForUser(Guid userId)
		{
			IList<VideoCardData> videoCards = new List<VideoCardData>();
			try
			{
                var user = await _httpService.Get<UserDto>("api/User/Get", "Id", userId);
				var videos = await _httpService.Get<IEnumerable<VideoDto>>("api/Video/GetAllForUser", "userId", userId);
                var currentUser = await _userService.GetCurrent();

				string apiUrl = _httpService.GetAPI();
				foreach (VideoDto video in videos)
				{
                    if (video.IsPublic || (currentUser != null && currentUser.Id == userId))
                    {
						videoCards.Add(new VideoCardData(
						    video.Id,
						    video.Title,
						    Path.Combine(apiUrl, video.ImagePath),
						    user.Username
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

        public async Task Delete(Guid id)
        {
            try
            {
                await _httpService.Delete<string>("api/Video/Delete", id);
            }
            catch
            {

            }
        }

        public async Task<VideoEditData?> GetVideoForEdit(string id)
        {
            try
            {
                VideoDto? videoDto = await _httpService.Get<VideoDto>("api/Video/Get", "id", id);

                if (videoDto == null)
                {
                    _navigationManager.NavigateTo("/", true);
                }

                string apiUrl = _httpService.GetAPI();
                return new VideoEditData()
                {
                    Id = videoDto.Id,
                    Title = videoDto.Title,
                    Description = videoDto.Description,
                    IsPublic = videoDto.IsPublic,
                    CategoryId = videoDto.CategoryId,
                    OldImagePath = Path.Combine(apiUrl, videoDto.ImagePath),
                    ImageData = null,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task EditVideo(VideoEditData data)
        {
            string? errorMessage = null;

            try
            {
                var success = await _httpService.PatchMultipart("api/Video/Edit", _multipartEditAdapter.Adapt(data));
                if (success)
                {
                    _navigationManager.NavigateTo($"/", true);
                }
                else
                {
                    throw new Exception("An error occured. Try again.");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            _navigationManager.NavigateTo($"/{errorMessage}");
        }
    }
}
