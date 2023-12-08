using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetBanking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpRequestController : ControllerBase
    {
        public InternetBankingContext context;

        public HelpRequestController(InternetBankingContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("create-help-request")]
        public async Task<IActionResult> RequestHelp(HelpRequest helpRequest)
        {
            if (ModelState.IsValid)
            {
                context.HelpRequests.Add(helpRequest);
                await context.SaveChangesAsync();
                return Ok(new { Message = "Help request submited successfully" });
            }
            return BadRequest(ModelState);
        }
        
        [HttpGet]
        [Route("view-help-request")]
        public async Task<IActionResult> GetHelpRequest()
        {
            var helpRequests = await context.HelpRequests.ToListAsync();
            return Ok(helpRequests);
        }
        
        private bool HelpRequestExists(int id)
        {
            return context.HelpRequests.Any(e => e.Id == id);
        }

        [HttpPut("{id}")]
        [Route("update-help-request/{id}")]
        public async Task<IActionResult> UpdateHelpRequest(int id, HelpRequest helpRequest)
        {
            if (id != helpRequest.Id)
            {
                return BadRequest();
            }
            
            context.Entry(helpRequest).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!HelpRequestExists(id))
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
        
        //delete: api/help/{id}
        [HttpDelete("{id}")]
        [Route("delete-help-request/{id}")]
        public async Task<IActionResult> DeleteHelpRequest(int id)
        {
            var helpRequest =await context.HelpRequests.FindAsync(id);
            if (helpRequest == null)
            {
                return NotFound("Not Found!");
            }

            context.HelpRequests.Remove(helpRequest);
            await context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpGet]
        [Route("search-help-request")]
        public async Task<ActionResult<IEnumerator<HelpRequest>>> SearchHelpRequest(string searchString)
        {
            var helpRequests = await context.HelpRequests.Where(h => h.Content.Contains(searchString)).ToListAsync();
            if (helpRequests == null)
            {
                return NotFound();
            }
            return Ok(helpRequests);
        }
    }
}
