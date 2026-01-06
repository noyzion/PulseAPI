namespace PulseAPI.Models.Responses
{
    public class UserTaskReportResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public int TotalTasks { get; set; }
        public double CompletionPercentage { get; set; }
    }
}
