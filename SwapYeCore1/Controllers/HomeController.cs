using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SwapYeCore1.Data;
using SwapYeCore1.Models;
using System.Linq;
using SwapYeCore1.ViewModels;
using System.Diagnostics;

namespace SwapYeCore1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SwapYeCoreContext _context;

        public HomeController(ILogger<HomeController> logger, SwapYeCoreContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            var items = _context.Items.Include(m => m.City)
                .Include(m => m.ItemType);
            //    .ToPagedList(page ?? 1, 6);

            if (items == null)
            {
                return NotFound();
            }

            return View(items);
		
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            var items = _context.Items
                .Where(x => x.ItemName.StartsWith(search))
                .Include(m => m.ItemType)
                .Include(m => m.City)
                .ToList();
            return View(items);
            
        }

    }
}