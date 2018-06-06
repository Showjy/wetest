using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wetest.Models;

namespace wetest.Controllers
{   



    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> BuyerPage()
        {
            var validItem = (from p in _context.Items
                             where p.Status == "valid"
                             select p);
            return View(await validItem.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> TesterPage()
        {
            string currentid = User.Identity.Name;
            
            var items = (from t in _context.Items 
                         from p in _context.UserToItem                         
                         where currentid == p.SellerId && p.ItemId==t.Id && t.Status == "valid"
                         select t);
            return View(await items.ToListAsync());
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
