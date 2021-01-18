using LibraryManagementSystem.Domain;
using LibraryManagementSystem.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;
          
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(dto.UserName);
           
                var signInResult =
                    await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, true);


                if (signInResult.Succeeded)
                {
                        return RedirectToAction("Index", "Home");
                }


                ModelState.AddModelError("", "An error occurred, please try again later");
                return View(dto);
            }

             return View(dto);
        }


        public async Task<IActionResult> ChangePassword(int id) 
        {
            ApplicationUser applicationUserToChangePassword = await _userManager.FindByIdAsync(id.ToString());
            if (applicationUserToChangePassword is null)
            {
                return NotFound();
            }

            UpdatePasswordDto updatePasswordDto = new()
            {
                ApplicationUserId = applicationUserToChangePassword.Id
            };
            return PartialView(updatePasswordDto);


        } 


        [HttpPost]
        public async Task<IActionResult> ChangePassword(UpdatePasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(dto.ApplicationUserId.ToString());

                IdentityResult changePasswordResult =
                     await _userManager.ChangePasswordAsync(applicationUser, dto.CurrentPassword, dto.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(applicationUser);
                    return Json(new { success = true });
                }

                else
                {
                    foreach (var erorr in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erorr.Description);
                    }

                    List<string> errors = new();
                    foreach (var modelStateValue in ModelState.Values.Where(m => m.Errors.Count > 0))
                    {
                        foreach (var item in modelStateValue.Errors)
                        {
                            errors.Add(item.ErrorMessage);
                        }

                    }

                    return Json(new { success = false, errors });
                }


            }
            else
            {
                return Json(new { success = false, errors = "An error occurred, Please try again later." });
            }
        }

    }
}
