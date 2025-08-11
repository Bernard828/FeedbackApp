using FeedbackAPI.Data;
using FeedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FeedbackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {

        private readonly FeedbackContext _context;


        public FeedbackController(FeedbackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFeedbacks), new { id = feedback.Id }, feedback);
        }
              
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
                return NotFound();

            return feedback;
        }
        
        //Filter feedbacks by role
        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetByRole(string role)
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.Role != null && f.Role.ToLower() == role.ToLower())
                    .ToListAsync();

            return feedbacks;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, Feedback updatedFeedback)
        {
            if (id != updatedFeedback.Id)
                return BadRequest();

            var existing = await _context.Feedbacks.FindAsync(id);
            if (existing == null)
                return NotFound();
            existing.Message = updatedFeedback.Message;
            existing.Role = updatedFeedback.Role;
            existing.GuestName = updatedFeedback.GuestName;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
                return NotFound();

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent(); //204 Status Code
        }
    }
}
