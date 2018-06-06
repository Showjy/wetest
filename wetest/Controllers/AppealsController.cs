using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.models;

namespace wetest.Controllers
{
    public class AppealsController : Controller
    {
        private readonly DataContext _context;

        public AppealsController(DataContext context)
        {
            _context = context;
        }



        public IActionResult OrderAppeal(string id)
        {
            ViewData["OrderId"] = id;
            return View();
        }



        public async Task<IActionResult> Create([Bind("OrderId,Information")] Models.viewmodels.AppealCreateViewModel appeal)
        {
            if (ModelState.IsValid)
            {
                Appeal newAppeal = new Appeal();
                newAppeal.Id = appeal.OrderId;
                newAppeal.OrderId = appeal.OrderId;
                newAppeal.Date = System.DateTime.Now.ToShortDateString();
                newAppeal.Information = appeal.Information;
                _context.Add(newAppeal);

                var os = (from p in _context.Orders
                          where p.Id == appeal.OrderId
                          select p);
                var o = os.FirstOrDefault();
                o.Status = "appeal";
                await _context.SaveChangesAsync();
                return Json("result=\"AppealSuccess\"");
            }
            return View(appeal);
        }

    }
}
