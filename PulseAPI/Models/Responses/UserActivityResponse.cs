using PulseAPI.Models.Enums;
namespace PulseAPI.Models.Responses
{
    public class UserActivityResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }    
        public int PostsCount { get; set; }
        public ActivityLevel ActivityLevelUser { get; set; }
    }


}
