namespace PulseAPI.Models.Responses
{
    public class UserSummaryResponse
    {
        public int UserId {  get; set; }
        public int UsersPosts { get; set; }
        public int UsersComments { get; set; }
        public double AvgCommentsPerPost { get; set; }

    }
}
