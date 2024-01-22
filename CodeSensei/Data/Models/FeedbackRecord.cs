namespace CodeSensei.Data.Models
{
    public class FeedbackRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserFeedback { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
