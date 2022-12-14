using FrontToBack103.DAL;
using FrontToBack103.Models;
using FrontToBack103.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FrontToBack103.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                sliders = _context.Sliders.ToList(),
                pageIntro = _context.PageIntros.FirstOrDefault(),
                categories = _context.Categories.ToList()
            };
           

            return View(homeVM);
        }
    }
}
