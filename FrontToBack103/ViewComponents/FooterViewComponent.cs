using FrontToBack103.DAL;
using FrontToBack103.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack103.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
