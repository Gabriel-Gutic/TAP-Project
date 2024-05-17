using BlazorClient.Data;

namespace BlazorClient.Contracts
{
    public interface IVideoCategoryService
    {
        public Task<IEnumerable<VideoCategoryData>?> GetAll();
    }
}
