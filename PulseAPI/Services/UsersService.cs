using PulseAPI.Client;
using PulseAPI.Models;
using PulseAPI.Models.External;
using PulseAPI.Models.Responses;

namespace PulseAPI.Services
{
    public class UsersService
    {
        private readonly JsonPlaceholderClient _client;
        public UsersService(JsonPlaceholderClient client)
        {
            _client = client;
        }

        public async Task<UserSummaryResponse?> GetUserSummaryAsync(int userId)
        {
            var user = await _client.GetUserByIdAsync(userId);

            if (user == null) return null;

            var posts = await _client.GetPostsByUserIdAsync(userId);
            var comments = await _client.GetCommentsAsync();
            var postIds = posts.Select(p => p.Id).ToHashSet();
            int countComments = comments.Count(c => postIds.Contains(c.PostId));
            var postsCount = posts.Count;
            double avgCommentsPerPost = postsCount > 0 ? (double)countComments / postsCount : 0;

            UserSummaryResponse response = new UserSummaryResponse
            {
                UserId = userId,
                UsersComments = countComments,
                UsersPosts = postsCount,
                AvgCommentsPerPost = avgCommentsPerPost
            };

            return response;
        }
    }
}
