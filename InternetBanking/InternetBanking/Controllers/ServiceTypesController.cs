using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Model;

namespace InternetBanking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        InternetBankingContext _context;

        public ServiceTypesController(InternetBankingContext context)
        {
            _context = context;
        }

        // GET: api/ServiceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServicesTypes()
        {
          if (_context.ServicesTypes == null)
          {
              return NotFound();
          }
            return await _context.ServicesTypes.ToListAsync();
        }

        // GET: api/ServiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceType>> GetServiceType(int? id)
        {
          if (_context.ServicesTypes == null)
          {
              return NotFound();
          }
            var serviceType = await _context.ServicesTypes.FindAsync(id);

            if (serviceType == null)
            {
                return NotFound();
            }

            return serviceType;
        }

        // PUT: api/ServiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(int? id, ServiceType serviceType)
        {
            if (id != serviceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceTypeExists(id))
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

        // POST: api/ServiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceType>> PostServiceType(ServiceType serviceType)
        {
          if (_context.ServicesTypes == null)
          {
              return Problem("Entity set 'InternetBankingContext.ServicesTypes'  is null.");
          }
            _context.ServicesTypes.Add(serviceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceType", new { id = serviceType.Id }, serviceType);
        }

        // DELETE: api/ServiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceType(int? id)
        {
            if (_context.ServicesTypes == null)
            {
                return NotFound();
            }
            var serviceType = await _context.ServicesTypes.FindAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }

            _context.ServicesTypes.Remove(serviceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceTypeExists(int? id)
        {
            return (_context.ServicesTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
