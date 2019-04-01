﻿using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataStorage.BLL.Interfaces;
using DataStorage.App.ViewModels;
using System;

namespace DataStorage.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _userService;

        public AccountController(IUsersService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = await _userService.GetUserAsync(model.Email, model.Password, model.rememberMe);
                if (login.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var register = await _userService.CreateUserAsync(model.Email, model.Password);
                if (register.Succeeded)
                {
                    await _userService.GetUserAsync(model.Email, model.Password, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return View(model);
        }

        /*public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _userService.LogOut();
            return RedirectToAction("Login", "Account");
        }*/
    }    
}