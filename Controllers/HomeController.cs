using LibraryManagementSystem.Domain;
using LibraryManagementSystem.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<IActionResult> Index()
        {
            var applicationUsers = await _userManager.Users.Select(u => new ApplicationUserDetailDto 
            { 
                Email = u.Email,
                FirstName = u.FirstName,
                ApplicationUserId = u.Id,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                UserName = u.UserName
            
            
            }).ToListAsync();

            return View(applicationUsers);
        }

        public IActionResult CreateUser() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateApplicationUserDto dto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newApplicationUser = new()
                {
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                    PhoneNumber = dto.PhoneNumber,
                };
                IdentityResult createUserResult =  await _userManager.CreateAsync(newApplicationUser, dto.Password);
                if (createUserResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(dto);
                }

            }
            return View(dto);
        }
    }
}
