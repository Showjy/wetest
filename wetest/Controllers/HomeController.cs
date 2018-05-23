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
            return View(await _context.Items.ToListAsync());
        }

        public ViewResult TesterPage()
        {

            return View("TesterPage");
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
