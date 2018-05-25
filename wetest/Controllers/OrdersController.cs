using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wetest.Models;

namespace wetest.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        public IActionResult CreateOrder(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var provider = (from p in _context.UserToItem
                              where p.ItemId == id
                              select p);
            var providerid = provider.First().SellerId;
            ViewData["ProviderId"] = providerid;
            ViewData["test"] = "test";
            return View();
        }

        
        public async Task<IActionResult> ShowMyOrders()
        {
            string id = User.Identity.Name;
            var orders = (from p in _context.Orders
                          where p.ProviderId == id || p.ServicerId == id
                          select p);
            ViewData["MyId"] = id;
            return View(await orders.ToListAsync());
        }

       
        public async Task<IActionResult> OrderRefuse(string id)
        {
            var orders = (from p in _context.Orders
                          where p.Id == id
                          select p);
            var order = orders.First();
            order.Status = "closed";
            await _context.SaveChangesAsync();
            return Json("result=\"RefuseOrderSuccess\"");
        }

        public async Task<IActionResult> OrderAccept(string id)
        {
            var orders = (from p in _context.Orders
                          where p.Id == id
                          select p);
            var order = orders.First();
            order.Status = "open";
            await _context.SaveChangesAsync();
            return Json("result=\"RefuseOrderSuccess\"");
        }

        public async Task<IActionResult> OrderFinish(string id)
        {
            var orders = (from p in _context.Orders
                          where p.Id == id
                          select p);
            var order = orders.First();
            order.Status = "finish";
            await _context.SaveChangesAsync();
            return Json("result=\"FinishOrderSuccess\"");
        }


        public async Task<IActionResult> ChangeProgress([Bind("Id,Progress")]Models.viewmodels.OrderChangeProgressViewModel orderProgress)
        {
            var orders = (from p in _context.Orders
                         where p.Id == orderProgress.Id
                         select p);
            var order = orders.First();
            order.progress = orderProgress.Progress;
            await _context.SaveChangesAsync();
            return Json("result=\"ModProgressSuccess\"");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProviderId,Price,StartDate,EndDate,Information")]  Models.viewmodels.OrderCreateViewModel createOrder)
        {
            if (ModelState.IsValid)
            {
                if (createOrder.ProviderId == User.Identity.Name)
                {
                    return Json("result=\"YouCannotRecieveYourOwnOrder\"");
                }
                Order order = new Order();
                order.EndDate = createOrder.EndDate;
                order.Information = createOrder.Information;
                order.Price = createOrder.Price;
                order.StartDate = createOrder.StartDate;

                var count = (from p in _context.Orders
                             select p).Count();
                string id = functions.Functions.ChangeCounttoId((long)count + 1);
                order.Id = id;
                order.progress = 0;
                order.Status = "waiting";
                order.ProviderId = createOrder.ProviderId;
                order.ServicerId = User.Identity.Name;
                _context.Add(order);

                await _context.SaveChangesAsync();
                return Json("result=\"CreateOrderSuccess\"");
            }
            return Json("result=\"CreateOrderFail\"");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Price,StartData,EndData,Information,Status")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
