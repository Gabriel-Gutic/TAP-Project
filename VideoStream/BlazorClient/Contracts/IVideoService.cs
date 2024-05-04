using BlazorClient.Objects;

namespace BlazorClient.Contracts
{
    public interface IVideoService
    {
        public Task<IEnumerable<Video>?> GetAll();
    }
}
