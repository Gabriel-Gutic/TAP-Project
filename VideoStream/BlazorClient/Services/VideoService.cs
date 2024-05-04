using BlazorClient.Contracts;
using BlazorClient.Objects;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class VideoService : IVideoService
    {
        private readonly HttpClient _httpClient;

        public VideoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Video>?> GetAll()
        {
            try
            {
                var videos = await _httpClient.GetFromJsonAsync<IEnumerable<Video>>("api/VideoSelector/Select");
                return videos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
