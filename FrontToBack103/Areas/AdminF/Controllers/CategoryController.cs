using FrontToBack103.DAL;
using FrontToBack103.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack103.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory==null) return NotFound();
            return View(dbCategory);
        }

        public IActionResult Delete()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory==null) return NotFound();
            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
