
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IUserService
    {
        public Task<UserData?> GetCurrent();
        public Task<UserData?> Get(Guid id);
    }
}
