using BlazorClient.Contracts;
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserData?> GetCurrent()
        {
            UserDto? userDto = await _httpService.Get<UserDto>("api/Auth/GetUser");
            if (userDto != null)
            {
                return new UserData(
                        userDto.Id,
                        userDto.Username,
                        userDto.Email,
                        userDto.Password,
                        userDto.ImagePath == null ? null : Path.Combine(_httpService.GetAPI(), userDto.ImagePath),
                        userDto.IsAdmin,
                        userDto.IsActive,
                        userDto.CreatedAt
                    );
            }
            return null;
        }

        public async Task<UserData?> Get(Guid id)
        {
            UserDto? userDto = await _httpService.Get<UserDto>("api/User/GetUser", "id", id);
            if (userDto == null)
            {
                return null;
            }

            return new UserData(
                        userDto.Id,
                        userDto.Username,
                        userDto.Email,
                        userDto.Password,
                        userDto.ImagePath == null ? null : Path.Combine(_httpService.GetAPI(), userDto.ImagePath),
                        userDto.IsAdmin,
                        userDto.IsActive,
                        userDto.CreatedAt
                    );
        }
    }
}
