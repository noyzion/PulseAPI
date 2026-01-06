using PulseAPI.Client;
using PulseAPI.Models.External;
using PulseAPI.Models.Responses;

namespace PulseAPI.Services
{
    public class AnalyticsService
    {
        private readonly JsonPlaceholderClient _client;
        public AnalyticsService(JsonPlaceholderClient client)
        {
            _client = client;
        }

        public async Task<GlobalStatsResponse> GetGlobalStatsAsync()
        {
            var comments = await _client.GetCommentsAsync();
            var posts = await _client.GetPostsAsync();
            var users = await _client.GetUsersAsync();

            if (users.Count == 0)
            {
                return new GlobalStatsResponse
                {
                    CommentsCount = 0,
                    PostCount = 0,
                    UserCount = 0,
                    MostActiveUser = null
                };
            }

            var postsCountByUser = posts.GroupBy(p => p.UserId).ToDictionary(g => g.Key, g => g.Count());

            ExternalUserDto mostActiveUser = users.First();
            int maxPosts = postsCountByUser.ContainsKey(mostActiveUser.Id) ? postsCountByUser[mostActiveUser.Id] : 0;

            foreach (var user in users)
            {
                int userPostsCount = postsCountByUser.ContainsKey(user.Id) ? postsCountByUser[user.Id] : 0;

                if (userPostsCount > maxPosts)
                {
                    maxPosts = userPostsCount;
                    mostActiveUser = user;
                }
            }

            return new GlobalStatsResponse
            {
                CommentsCount = comments.Count,
                PostCount = posts.Count,
                UserCount = users.Count,
                MostActiveUser = mostActiveUser
            };
        }
    }
}
