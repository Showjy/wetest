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

        public async Task<IActionResult> Index()
        {
            var managers = (from p in _context.Users
                            where p.Level == 0 || p.Level == 1
                            select p);

            string uid = User.Identity.Name;
            var level = (from p in _context.Users
                         where p.Id == uid
                         select p).FirstOrDefault().Level;
            ViewData["UserLevel"] = level;
            return View(await managers.ToListAsync());
        }




        public async Task<IActionResult> ItemIndex()
        {
            return View(await _context.Items.ToListAsync());
        }

        public async Task<IActionResult> OrderIndex()
        {
            return View(await _context.Orders.ToListAsync());

        }
    

        [HttpPost]
        public async Task<IActionResult> ChangeAppealStatus(string id)
        {
            var appeal = (from p in _context.Appeal
                         where p.Id == id
                         select p).First();
            appeal.Status = "invalid";
            await _context.SaveChangesAsync();
            return Json("result=\"ChangeStatusSuccess\"");

        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus([Bind("Id,Status")] Models.viewmodels.OrderChangeStatusViewModel orderStatus)
        {
            var order = (from p in _context.Orders
                         where p.Id == orderStatus.Id
                         select p).FirstOrDefault();
            order.Status = orderStatus.Status;
            await _context.SaveChangesAsync();
            return Json("result=\"ChangeStatusSuccess\"");

        }

        public async Task<IActionResult> AppealIndex()
        {
            return View(await _context.Appeal.ToListAsync());
        }



        public IActionResult OrderAppeal(string id)
        {
            ViewData["OrderId"] = id;
            return View();
        }

        public IActionResult Edit(string id)
        {
            var user = (from p in _context.Users
                        where p.Id == id
                        select p).FirstOrDefault();
            user.Level = 2;
            _context.SaveChanges();
            return Json("result=\"EditSuccess\"");
        }



        public async Task<IActionResult> Create([Bind("OrderId,Information")] Models.viewmodels.AppealCreateViewModel appeal)
        {
            if (ModelState.IsValid)
            {
                var exist = (from p in _context.Appeal
                             where p.OrderId == appeal.OrderId
                             select p);
                if (exist.Any())
                {
                    return Json("result=\"AlreadyAppeal\"");
                }
                Appeal newAppeal = new Appeal();
                newAppeal.Id = appeal.OrderId;
                newAppeal.OrderId = appeal.OrderId;
                newAppeal.Date = System.DateTime.Now.ToShortDateString();
                newAppeal.Information = appeal.Information;
                newAppeal.Status = "valid";
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
