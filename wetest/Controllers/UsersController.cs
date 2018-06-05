using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wetest.Models;
using wetest.Models.models;
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
       
       
        public string CurrentUser()
        {
            return User.Identity.Name;
        }
        

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Name,Password")]UserRegistViewModel userLogin)
        {
            
            var password = (from p in _context.Users
                           where p.Name == userLogin.Name
                           select p.Password);
            var ids = (from p in _context.Users
                            where p.Name == userLogin.Name
                            select p.Id);
            string id = ids.FirstOrDefault();
            if (password.Count()==1)
            {
                if (password.Contains(userLogin.Password))
                {

                    List<Claim> claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, id)
                    };

                    // create identity
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                    // create principal
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // sign-in
                    await HttpContext.SignInAsync(
                            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                            principal: principal
                            );
                    return Json("result=\"LoginSuccess\"");
                }
                else {
                    return Json("result=\"NoSuchUser\"");
                }   
            }
            return NotFound();
            
        }       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("../Home/LoginPage");
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
                    //添加User
                    string id = functions.Functions.ChangeCounttoId((long)count + 1);
                    user.Id = id;
                    user.Name = userRegist.Name;
                    user.Password = userRegist.Password;
                    _context.Add(user);
                    

                    //初始化UserInfo
                    UserInfo info = new UserInfo();
                    info.Id = id;
                    info.Email = "";
                    info.PhoneNumber = "";
                    info.RegistDate = System.DateTime.Now.ToShortDateString();
                    _context.Add(info);
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


        public async Task<IActionResult> ChangePassword([Bind("Before,After")] Models.viewmodels.PasswordChangeViewModel passwordChange)
        {
            var id = User.Identity.Name;
            var users = (from p in _context.Users
                        where p.Id == id
                        select p);
            var user = users.FirstOrDefault();
            if (passwordChange.Before == user.Password)
            {
                user.Password = passwordChange.After;
                await _context.SaveChangesAsync();

                return Json("result=\"EditPasswordSuccess\"");

            }
            else
            {
                return Json("result=\"EditPasswordFail\"");

            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Password")] User user)
        {         
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

        public string NameToId(string name)
        {
            var ids = (from p in _context.Users
                       where p.Name == name
                       select p);
            var id = ids.FirstOrDefault().Id;
            return id;
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
