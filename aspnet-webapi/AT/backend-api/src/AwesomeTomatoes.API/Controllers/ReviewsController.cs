using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeTomatoes.BLL.Models;
using WebAwesomeTomatoes.Models;
using Microsoft.AspNetCore.Authorization;

namespace AwesomeTomatoes.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly EFContext _context;

    public ReviewsController(EFContext context)
    {
        _context = context;
    }

    // GET: api/Reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return await _context.Reviews.Include(r => r.Movie)
                .AsNoTracking()
                .OrderByDescending(r => r.ReviewDate).ToListAsync();
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _context.Set<Review>()
                             .Include(m => m.Movie)
                             .FirstOrDefaultAsync(m => m.Id == id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // PUT: api/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, Review review)
    {
        if (id != review.Id)
        {
            return BadRequest();
        }

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReviewExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReview", new { id = review.Id }, review);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReviewExists(int id)
    {
        return _context.Reviews.Any(e => e.Id == id);
    }
}
