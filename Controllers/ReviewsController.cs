using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ReviewsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            return await _context.Reviews.Include(x => x.Book).Include(x => x.User).ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviews(int id)
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            var reviews = await _context.Reviews.FindAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(int id, Review reviews)
        {
            if (id != reviews.Id)
            {
                return BadRequest();
            }

            _context.Entry(reviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
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
        public async Task<ActionResult<Review>> PostReviews(Review reviews)
        {
          if (_context.Reviews == null)
          {
              return Problem("Entity set 'AppDBContext.Reviews'  is null.");
          }
            _context.Reviews.Add(reviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(int id)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewsExists(int id)
        {
            return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
