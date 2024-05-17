using BlazorClient.Contracts;
using BlazorClient.Data;

namespace BlazorClient.Services
{
    public class VideoCategoryService : IVideoCategoryService
    {
        private readonly IHttpService _httpService;

        public VideoCategoryService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<VideoCategoryData>?> GetAll()
        {
            var categories = await _httpService.Get<IEnumerable<VideoCategoryData>>("api/VideoCategory/GetAll"); 
            return categories == null || categories.Count() == 0 ? null : categories;
        }
    }
}
