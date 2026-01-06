using PulseAPI.Models.External;

namespace PulseAPI.Models.Responses
{
    public class GlobalStatsResponse
    {
       public int PostCount {  get; set; }
        public int UserCount { get; set; }

        public int CommentsCount { get; set; }
        public ExternalUserDto MostActiveUser {  get; set; }
    }
}
