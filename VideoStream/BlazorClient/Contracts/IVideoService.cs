
using BlazorClient.Data;
using BlazorClient.Dto;

namespace BlazorClient.Contracts
{
    public interface IVideoService
    {
        public Task<IEnumerable<VideoCardData>?> SelectForUser();
        public Task<VideoData?> GetVideo(string id);
        public Task<VideoEditData?> GetVideoForEdit(string id);
        public Task EditVideo(VideoEditData data);
        public Task UploadVideo(VideoUploadData data);
        public Task<IEnumerable<VideoCardData>?> GetAllForUser(Guid userId);
        public Task Delete(Guid id);
    }
}
