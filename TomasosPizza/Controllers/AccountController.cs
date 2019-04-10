using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizza.ViewModels;
using TomasosPizza.Models;
using TomasosPizza.ModelsIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizza.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        private TomasosContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TomasosContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CustomerViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Home");
                }

                return View();
            }
            else
            {
                return View();
            }

        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userIdentity = new ApplicationUser { UserName = user.UserName };

                var result = await _userManager.CreateAsync(userIdentity, user.Password);

                user.CurrentCustomer.AspNetUserId = userIdentity.Id.ToString();

                _context.Kunds.Add(user.CurrentCustomer);
                _context.SaveChanges();

                var resultRole = await _userManager.AddToRoleAsync(userIdentity, user.RoleName);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(userIdentity, isPersistent: false);
                    RedirectToAction("HomePage", "Home");
                }

                ModelState.Clear();

                return View();
            }
            else
            {
                return View();
            }

        }
    }
}
