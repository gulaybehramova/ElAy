using FrontToBack103.DAL;
using FrontToBack103.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FrontToBack103.Controllers
{
   
    public class ProductController : Controller
    {
        private AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.ProductCount = _context.Products.Count();
            List<Product> products = _context.Products.Include(p => p.Category).Take(2).ToList();
            return View(products);
           
        }
        public IActionResult LoadMore(int skip)
        {

            List<Product> products = _context.Products.Include(p=>p.Category).Skip(skip).Take(2).ToList();
            return PartialView("_ProductPartial",products);
        }
    }
}
