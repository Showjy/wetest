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
    public class UserToItemsController : Controller
    {
        private readonly DataContext _context;

        public UserToItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: UserToItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserToItem.ToListAsync());
        }

        // GET: UserToItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToItem = await _context.UserToItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userToItem == null)
            {
                return NotFound();
            }

            return View(userToItem);
        }

        // GET: UserToItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserToItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SellerId,ItemId")] UserToItem userToItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userToItem);
        }

        // GET: UserToItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToItem = await _context.UserToItem.SingleOrDefaultAsync(m => m.Id == id);
            if (userToItem == null)
            {
                return NotFound();
            }
            return View(userToItem);
        }

        // POST: UserToItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,SellerId,ItemId")] UserToItem userToItem)
        {
            if (id != userToItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserToItemExists(userToItem.Id))
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
            return View(userToItem);
        }

        // GET: UserToItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToItem = await _context.UserToItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userToItem == null)
            {
                return NotFound();
            }

            return View(userToItem);
        }

        // POST: UserToItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userToItem = await _context.UserToItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserToItem.Remove(userToItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserToItemExists(string id)
        {
            return _context.UserToItem.Any(e => e.Id == id);
        }
    }
}
