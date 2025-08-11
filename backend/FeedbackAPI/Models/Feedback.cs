namespace FeedbackAPI.Models
{

    public class Feedback
    {

        public int Id { get; set; }
        public string? GuestName { get; set; }
        public string? Message { get; set; }
        public string? Role { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}