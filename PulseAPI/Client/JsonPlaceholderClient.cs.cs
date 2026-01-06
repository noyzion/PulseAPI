using System.Net.Http.Json;
using PulseAPI.Models.External;

namespace PulseAPI.Client
{
    public class JsonPlaceholderClient
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExternalUserDto>> GetUsersAsync()
        {
            var users = await _httpClient.GetFromJsonAsync<List<ExternalUserDto>>("/users");
            return users ?? new List<ExternalUserDto>();
        }

        public async Task<ExternalUserDto?> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ExternalUserDto>($"/users/{id}");
        }

        public async Task<List<ExternalPostDto>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _httpClient.GetFromJsonAsync<List<ExternalPostDto>>($"/posts?userId={userId}");
            return posts ?? new List<ExternalPostDto>();
        }

        public async Task<List<ExternalCommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _httpClient.GetFromJsonAsync<List<ExternalCommentDto>>($"/comments?postId={postId}");
            return comments ?? new List<ExternalCommentDto>();
        }
    }
}
