using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wetest.Models;

namespace wetest.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return View();
        }


        public ViewResult TesterPage()
        {

            return View("TesterPage");
        }
        [Route("Home/BuyerPage")]
        public ViewResult BuyerPage()
        {

            return View("BuyerPage");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
