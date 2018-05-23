using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.relatmodels;

namespace wetest.Controllers
{
    public class UserToOrdersController : Controller
    {
        private readonly DataContext _context;

        public UserToOrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: UserToOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserToOrder.ToListAsync());
        }

        // GET: UserToOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToOrder = await _context.UserToOrder
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userToOrder == null)
            {
                return NotFound();
            }

            return View(userToOrder);
        }

        // GET: UserToOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserToOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SellerId,BuyerId,OrderId")] UserToOrder userToOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userToOrder);
        }

        // GET: UserToOrders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToOrder = await _context.UserToOrder.SingleOrDefaultAsync(m => m.Id == id);
            if (userToOrder == null)
            {
                return NotFound();
            }
            return View(userToOrder);
        }

        // POST: UserToOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,SellerId,BuyerId,OrderId")] UserToOrder userToOrder)
        {
            if (id != userToOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserToOrderExists(userToOrder.Id))
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
            return View(userToOrder);
        }

        // GET: UserToOrders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToOrder = await _context.UserToOrder
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userToOrder == null)
            {
                return NotFound();
            }

            return View(userToOrder);
        }

        // POST: UserToOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userToOrder = await _context.UserToOrder.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserToOrder.Remove(userToOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserToOrderExists(string id)
        {
            return _context.UserToOrder.Any(e => e.Id == id);
        }
    }
}
