using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.relatmodels;

namespace wetest.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var contacts = (from p in _context.UserToItem
                            from t in _context.UserInfo
                            where p.ItemId == id && p.SellerId == t.Id
                            select t);
            var contact = contacts.FirstOrDefault();
            ViewData["PhoneNumber"] = contact.PhoneNumber;
            ViewData["Email"] = contact.Email;
        
            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]       
        public async Task<IActionResult> Create([Bind("Title,Information")] Models.viewmodels.ItemCreateViewModel item)
        {
            if (ModelState.IsValid)
            {
                Item newItem = new Item();
                var count = (from p in _context.Items
                             select p).Count();
                //添加User
                string id = functions.Functions.ChangeCounttoId((long)count + 1);
                newItem.Id = id;
                newItem.Title = item.Title;
                newItem.Information = item.Information;
                newItem.status = "valid";
                _context.Add(newItem);

                UserToItem uti = new UserToItem();
                var count2 = (from p in _context.UserToItem
                             select p).Count();
                string id2= functions.Functions.ChangeCounttoId((long)count2 + 1);
                uti.Id = id2;
                uti.ItemId = id;
                uti.SellerId = User.Identity.Name;
                _context.Add(uti);

                await _context.SaveChangesAsync();
                return Json("result=\"CreateItemSuccess\""); 
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        //public async Task<IActionResult> Edit([Bind("Title,Information")] Models.viewmodels.ItemCreateViewModel itemEdit)
        //{
        //    if (id != item.Id)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(item);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ItemExists(item.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(item);
        //}

        // GET: Items/Delete/5
        //public async Task<IActionResult> DeleteItem(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Items
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(item);
        //}

        // POST: Items/Delete/5
        [HttpPost]
        public IActionResult ChangeToInvalid(string id)
        {
            var items = (from p in _context.Items
                         where id == p.Id
                         select p);
            var item = items.FirstOrDefault();
            item.status = "Invalid";
            _context.SaveChanges();
            return Json("result=\"DeleteItemSuccess\"");
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
