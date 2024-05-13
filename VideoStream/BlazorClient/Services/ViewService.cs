using BlazorClient.Contracts;
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class ViewService : IViewService
    {
        private readonly IHttpService _httpService;
        private readonly IUserService _userService;

        public ViewService(IHttpService httpService, IUserService userService)
        {
            _httpService = httpService;
            _userService = userService;
        }

        public async Task<int> Count(Guid videoId)
        {
            return await _httpService.Get<int>("api/View/Count", "videoId", videoId);
        }

        public async Task CreateView(Guid videoId)
        {
            UserData? user = await _userService.GetCurrent();

            if (user == null)
            {
                return;
            }

            await _httpService.Post<string>("api/View/Insert", new NewViewDto(
                    user.Id,
                    videoId
                ));
        }
    }
}
