﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.viewmodels;

namespace wetest.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([Bind("Name,Password")]UserRegistViewModel userLogin)
        {
            var password = (from p in _context.Users
                           where p.Name == userLogin.Name
                           select p.Password);           
            if (password.Count()==1)
            {
                if (password.Contains(userLogin.Password))
                {

                    List<Claim> claims = new List<Claim>
                    {

                     new Claim(ClaimTypes.Name, userLogin.Name)
                    };


                    // create identity
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                    // create principal
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // sign-in
                    await HttpContext.SignInAsync(
                            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                            principal: principal,
                            properties: new AuthenticationProperties
                            {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                            }
                            );


                    return RedirectToPage("/BuyerPage","Home");

                }
                else {
                    return Json("result=\"NoSuchUser\"");
                }
                
            }
            
            return Json("result=\"LoginFail\";user=\""+userLogin.Name+"\"");
            
        }

       
   
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }





        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Password")] Models.viewmodels.UserRegistViewModel userRegist)
        {
           
            User user = new User();

            if (ModelState.IsValid)
            {   var ifexist = (from p in _context.Users
                             where p.Name == userRegist.Name select p);
                if (!ifexist.Any())
                {
                    var count = (from usr in _context.Users
                                 select usr).Count();

                    string id = functions.Functions.ChangeCounttoId((long)count + 1);
                    user.Id = id;
                    user.Name = userRegist.Name;
                    user.Password = userRegist.Password;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return Json("result=\"CreateUserSuccess\"");
                  
                }
                else
                {
                    return Json("result=\"ExistUser\"");
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Contect")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}