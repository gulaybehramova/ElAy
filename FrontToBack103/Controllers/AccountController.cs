using FrontToBack103.Helpers;
using FrontToBack103.Models;
using FrontToBack103.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FrontToBack103.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController( RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser newUser = new AppUser()
            {
                Fullname=register.Fullname,
                UserName=register.UserName,
                Email=register.Email
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                   
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());
             await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("index","home");
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult>Login(LoginVM login,string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser dbUser =await  _userManager.FindByEmailAsync(login.Email);
            if (dbUser==null)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View(login);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(dbUser, login.Password, login.RememberMe, true);


            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "your account is lockout");
                return View(login);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "email or password wrong");
                return View(login);
            }
            foreach (var item in await _userManager.GetRolesAsync(dbUser))
            {
                if (item.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToAction("index", "dashboard",new {area="adminF"});
                }
            }

            if (ReturnUrl==null)
            {
                return RedirectToAction("index", "home");
            }
            return Redirect(ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name=item.ToString() });
                }
            }
        }
    }
}
