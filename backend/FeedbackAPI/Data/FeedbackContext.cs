using FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAPI.Data
{
    public class FeedbackContext :DbContext
    {

       public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options) {}

       public  DbSet<Feedback> Feedbacks => Set<Feedback>();
    }
}
