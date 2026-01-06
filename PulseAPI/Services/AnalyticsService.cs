using PulseAPI.Client;
using PulseAPI.Models.Enums;
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

        public async Task<List<UserActivityResponse>> GetUsersActivityAsync()
        {
            var posts = await _client.GetPostsAsync();
            var users = await _client.GetUsersAsync();
            var postsCountPerUsers = posts.GroupBy(u => u.UserId).ToDictionary(g => g.Key, g => g.Count());
            List<UserActivityResponse> response = new List<UserActivityResponse>();
            foreach (var user in users)
            {
                int postsCount = postsCountPerUsers.ContainsKey(user.Id) ? postsCountPerUsers[user.Id] : 0;
                ActivityLevel userLevel = GetActivityLevel(postsCount);
                response.Add(new UserActivityResponse { 
                    UserId = user.Id, UserName = user.Name, ActivityLevelUser = userLevel, PostsCount = postsCount});

            }
            return response;
        }

        private ActivityLevel GetActivityLevel(int postsCount)
        {
            if (postsCount >= 0 && postsCount < 3) return ActivityLevel.Low;
            else if (postsCount >= 3 && postsCount < 6) return ActivityLevel.Medium;
            else return ActivityLevel.High;
        }

        public async Task<List<UserActivityResponse>> GetNTopUsersAsync(int limit)
        {

            var posts = await _client.GetPostsAsync();
            var users = await _client.GetUsersAsync();
            var postsCountPerUsers = posts.GroupBy(p => p.UserId).ToDictionary(g => g.Key, g => g.Count());
            var topUsers = postsCountPerUsers.OrderByDescending(x => x.Value).Take(limit);

            List<UserActivityResponse> response = new();

            foreach (var kvp in topUsers)
            {
                var user = users.First(u => u.Id == kvp.Key);
                int postsCount = kvp.Value;

                response.Add(new UserActivityResponse
                {
                    UserId = user.Id,
                    UserName = user.Name,
                    PostsCount = postsCount,
                    ActivityLevelUser = GetActivityLevel(postsCount)
                });
            }
            
            return response;
        }
    }
}
