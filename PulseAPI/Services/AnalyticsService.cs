using PulseAPI.Client;
using PulseAPI.Models.Responses;

namespace PulseAPI.Services
{
    public class AnalyticsService
    {
        public readonly JsonPlaceholderClient _client;

        public AnalyticsService(JsonPlaceholderClient client)
        {
            _client = client;
        }

        public async Task<GlobalStatsResponse> GetGlobalStatsAsync()
        {

        }
    }
}
