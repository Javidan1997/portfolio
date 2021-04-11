

using Eduhome.Areas.Manage.ViewModels;
using Eduhome.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eduhome.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task Create()
        {
            AppUser user = new AppUser
            {
                UserName = "Superadmin",
                FullName = "Super Admin",
            };

            await _userManager.CreateAsync(user, "Admin123");
            await _userManager.AddToRoleAsync(user, "Admin");

        }


        public async Task CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser admin = await _userManager.FindByNameAsync(loginVM.UserName);

            if (admin == null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect!");
                return View();
            }

            if (await _userManager.CheckPasswordAsync(admin, loginVM.Password))
            {
                ModelState.AddModelError("", "Username or Password is incorrect!");
                return View();
            }


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,admin.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            }, "Admin_Auth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("Admin_Auth", claimsPrincipal);

            return RedirectToAction("index", "dashboard");
        }
    }
}