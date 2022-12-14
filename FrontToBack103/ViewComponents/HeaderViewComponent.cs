using FrontToBack103.DAL;
using FrontToBack103.Models;
using FrontToBack103.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack103.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;

        public HeaderViewComponent(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager=userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            int totalCount = 0;

            if (Request.Cookies["basket"]!=null)
            {
                List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);
               

                foreach (var item in products)
                {
                    totalCount+=item.Count;

                }
               
            }
            ViewBag.BasketLength=totalCount;

            Bio bio = _context.Bios.FirstOrDefault();

            if (User.Identity.IsAuthenticated)
            {
                AppUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Fullname=currentUser.Fullname;
            }


            return View(await Task.FromResult(bio));
        }
    }
}
