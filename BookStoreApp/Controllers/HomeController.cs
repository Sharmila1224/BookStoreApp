using BookStoreApp.Models;
using BookStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        //  Constructor (VERY IMPORTANT)
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Index method
        public IActionResult Index()
        {
            var books = _context.Books
                                .Include(b => b.Author)
                                .ToList();

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}