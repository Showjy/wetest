using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.models;

namespace wetest.Controllers
{
    [Authorize]
    public class UserInfoesController : Controller
    {
        private readonly DataContext _context;

        public UserInfoesController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: UserInfoes
        public async Task<IActionResult> Index()
        {        
            return View(await _context.UserInfo.ToListAsync());
        }

        //// GET: UserInfoes/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userInfo = await _context.UserInfo
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (userInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userInfo);
        //}

        public IActionResult CurrentUser()
        {
            string id = User.Identity.Name;
            //var names = (from p in _context.Users
            //           where p.Id == id
            //           select p.Name);
            //string currentname = names.FirstOrDefault();

            var userInfo = (from p in _context.UserInfo
                            where id == p.Id
                            select p);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo.FirstOrDefault());
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("/UserInfoes/Details/{id?}")]
        public IActionResult Details(string id)
        {
            //string name = User.Identity.Name;
            //var ids = (from p in _context.Users
            //           where p.Name == name
            //           select p.Id);
            string currentid = User.Identity.Name;
            if(currentid != id)
            {
                return NotFound();
            }
            var userInfo = (from p in _context.UserInfo
                            where id == p.Id
                            select p);
            //if (userInfo == null)
            //{
            //    return NotFound();
            //}

            return View(userInfo.FirstOrDefault());
        }

        // GET: UserInfoes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserInfoes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Email,RegistDate")] UserInfo userInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userInfo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userInfo);
        //}

        // GET: UserInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return View(userInfo);
        }

      

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("PhoneNumber,Email")] Models.viewmodels.UserinfoEditViewModel userInfo)
        {
            
            if (ModelState.IsValid)
            {
                UserInfo info = new UserInfo();
                //string name = User.Identity.Name;
                //var ids = (from p in _context.Users
                //           where p.Name == name
                //           select p.Id);
                string id = User.Identity.Name;
                if (UserInfoExists(id))
                {
                    var user = (from p in _context.UserInfo
                                   where id == p.Id
                                   select p);
                    user.First().PhoneNumber = userInfo.PhoneNumber;
                    user.First().Email = userInfo.Email;
                    await _context.SaveChangesAsync();

                    return Json("result=\"EditUserInfoSuccess\"");
                    
                }
                else
                {
                    return Json("result=\"DonotFindUserID\"");

                }
            }
            return Json("result=\"EditInfoFail\"");
        }

        // GET: UserInfoes/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userInfo = await _context.UserInfo
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (userInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userInfo);
        //}

        //// POST: UserInfoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var userInfo = await _context.UserInfo.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.UserInfo.Remove(userInfo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool UserInfoExists(string id)
        {
            return _context.UserInfo.Any(e => e.Id == id);
        }
    }
}
