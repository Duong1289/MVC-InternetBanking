using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{
    // [Authorize]
    public class FaqCategoryController : Controller
    {
        private readonly InternetBankingContext ctx;

        public FaqCategoryController(InternetBankingContext ctx)
        {
            this.ctx = ctx;
        }

        // GET: FaqCategory
        public async Task<IActionResult> Index()
        {
            var cate = await ctx.FAQCategories!.ToListAsync();
            ViewBag.Result = false;
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"];
            }
            return View(cate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCategory faqcate)
        {
            ctx.FAQCategories!.Add(faqcate);
            if (await ctx.SaveChangesAsync() >0)
            {
                TempData["Result"] = "Create FAQ-Category succesfully";
                return RedirectToAction("Index");
            }
            ViewBag.Result = false;
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var faq = await ctx.FAQCategories!.SingleOrDefaultAsync(f => f.Id == id);
            if (faq == null)
            {
                TempData["Result"] = "Can not find FAQ-Category question";
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQCategory cate)
        {
            if (ModelState.IsValid)
            {
                ctx.Entry(cate).State = EntityState.Modified;
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
            var faq = await ctx.FAQCategories!.SingleOrDefaultAsync(f => f.Id == id);
            if (faq != null)
            {
                ctx.FAQCategories!.Remove(faq);
                if (await ctx.SaveChangesAsync() > 0)
                {
                    TempData["Result"] = "Delete FAQ-Category succesfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Result = false;
            return View(faq);
        }






    }
}
