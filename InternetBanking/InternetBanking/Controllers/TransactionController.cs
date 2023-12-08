using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetBanking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private InternetBankingContext context;

        public TransactionController(InternetBankingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("view-statement")]
        public async Task<IActionResult> ViewStatement([FromQuery] string accountNumber, [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var statement = await context.Transactions
                    .Where(t => (t.SenderAccountNumber == accountNumber || t.ReceiverAccountNumber == accountNumber) &&
                                t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                    .OrderByDescending(t => t.TransactionDate).ToListAsync();
                if (statement == null)
                {
                    return NotFound();
                }
                return Ok(statement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("view-statement/{id}")]
        public async Task<IActionResult> GetStatementById(int id)
        {
            try
            {
                var statement = await context.Transactions.FindAsync(id);
                if (statement == null)
                {
                    return NotFound("Statement not found");
                }

                return Ok(statement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }


    }
}
