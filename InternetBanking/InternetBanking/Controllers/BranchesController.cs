using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Model;

namespace InternetBanking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        InternetBankingContext _context;

        public BranchesController(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<List<Branch>>> GetBranches()
        {
            //if (_context.Branches == null)
            //{
            //    return NotFound();
            //}
            //  return Ok( await _context.Branches.ToListAsync());
            var branches = await _context.Branches
           .Include(b => b.Employees) // Include the Employees navigation property
           .ToListAsync();

            if (branches == null)
            {
                return NotFound();
            }

            return Ok(branches);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(int? id)
        {
          if (_context.Branches == null)
          {
              return NotFound();
          }
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int? id, Branch branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
          if (_context.Branches == null)
          {
              return Problem("Entity set 'InternetBankingContext.Branches'  is null.");
          }
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int? id)
        {
            if (_context.Branches == null)
            {
                return NotFound();
            }
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchExists(int? id)
        {
            return (_context.Branches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
