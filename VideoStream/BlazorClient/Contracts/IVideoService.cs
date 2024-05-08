
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IVideoService
    {
        public Task<IEnumerable<VideoCardData>?> SelectForUser();
        public Task<VideoData?> GetVideo(string id);
    }
}
