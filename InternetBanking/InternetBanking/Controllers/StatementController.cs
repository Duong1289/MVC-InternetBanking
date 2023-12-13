using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using InternetBanking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Controllers
{
    public class StatementController : Controller
    {
        private InternetBankingContext _context;

        public StatementController(InternetBankingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve all statements
            var statements = await _context.Transactions.OrderByDescending((t => t.TransactionDate)).ToListAsync();
            
            return View(statements);
        }

        public async Task<IActionResult> CreateStatement()
        {
            return View();
        }

        
        // Action to view statements within a certain period
        public async Task<IActionResult> StatementsWithinPeriod(DateTime startDate, DateTime endDate)
        {
            // Retrieve statements within the specified period
            var statements = await _context.Transactions
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return View(statements);
        }
        
        [HttpPost]
        public async Task<IActionResult> ExportToXml(DateTime startDate, DateTime endDate)
        {
            var statements = await _context.Transactions!
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            var serializer = new XmlSerializer(typeof(List<Transaction>));

            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, statements);

                Response.Headers.Add("Content-Disposition", "attachment; filename=statements.xml");
                Response.Headers.Add("Content-Type", "application/xml");

                return File(memoryStream.ToArray(), "application/xml");
            }
        }

        
        
        
    }
}