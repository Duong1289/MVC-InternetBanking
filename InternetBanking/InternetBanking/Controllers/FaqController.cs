using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternetBanking.Controllers
{
    [Authorize]
    public class FaqController : Controller
    {
        private readonly InternetBankingContext ctx;

        public FaqController(InternetBankingContext ctx)
        {
            this.ctx = ctx;
        }

        // GET: Faq
        public async Task<IActionResult> Index()
        {
            ViewBag.Result = false;
            if (TempData["Result"]!=null)
            {
                ViewBag.Result = TempData["Result"];
            }
            var faqs = await ctx.FAQ!.Include(faq => faq.FAQCategory).ToListAsync();
           
            return View(faqs);

        }


        public async Task<IActionResult> Create()
        {
            ViewBag.FAQcategory = await ctx.FAQCategories!.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQ faq)
        {

            ctx.FAQ!.Add(faq);
            
            if(await ctx.SaveChangesAsync()>0)
            {
                TempData["Result"] = "Create FAQ succesfully"; 
                return RedirectToAction("Index");
            }
            ViewBag.Result = false;
            return View();
   
        }

        public async Task<IActionResult>Update(int id)
        {
            var faq = await ctx.FAQ!.SingleOrDefaultAsync(f=>f.Id == id);
            ViewBag.FAQcategory = await ctx.FAQCategories!.ToListAsync();
            if (faq ==null)
            {
                TempData["Result"] = "Can not find FAQ question";
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQ faq)
        {
            if(ModelState.IsValid)
            {
                ctx.Entry(faq).State = EntityState.Modified;
                if (await ctx.SaveChangesAsync() > 0)
                {
                    TempData["Result"] = "Update successfully!";
                    return RedirectToAction("Index");
                }
                ViewBag.Result = false;
                return View();
            }
            ViewBag.Result = false;
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var faq = await ctx.FAQ!.SingleOrDefaultAsync(f => f.Id == id);
            if (faq != null)
            {
                ctx.FAQ!.Remove(faq);
                if (await ctx.SaveChangesAsync() > 0)
                {
                    TempData["Result"] = "Delete FAQ succesfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Result = false;
            return View(faq);
        }


    }
}
