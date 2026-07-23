using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;
using TodoAPI.Requests;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly TodoContext _context;
    public ActivityController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/Activity
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivity() => 
        await _context.Activity.ToListAsync();

    // GET: api/Activity/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(string id)
    {
        var Activity = await _context.Activity.FindAsync(id);

        if (Activity == null)
        {
            return NotFound();
        }

        return Activity;
    }

    // PUT: api/Activity/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(string? id, Activity Activity)
    {
        if (id != Activity.Id)
        {
            return BadRequest();
        }

        _context.Entry(Activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivityExists(id))
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

    // POST: api/Activity
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Activity>> PostActivity(CreateActivityRequest Activity)
    {
        var newItem = new Activity
        {
            Id = Guid.NewGuid().ToString(),
            IsComplete = Activity.IsComplete,
            Name = Activity.Name,

        };
        _context.Activity.Add(newItem);
        await _context.SaveChangesAsync();

        return Created(nameof(GetActivity), newItem.Id);
    }

    // DELETE: api/Activity/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string? id)
    {
        var activity = await _context.Activity.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        _context.Activity.Remove(activity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActivityExists(string? id)
    {
        return _context.Activity.Any(e => e.Id == id);
    }
}
