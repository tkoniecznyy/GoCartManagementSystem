using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models.DTO;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace GoCart_ManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountService _accountService;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext dbContext, IAccountService accountService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _accountService = accountService;
        }


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto dto)
        {
            // string token = _accountService.GenerateJwt(dto);
            try
            {
                var claims = _accountService.GenerateClaimsAndAuthenticate(dto);

                if (claims.Any())
                {


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // This is optional configuration
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                    };
                    await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity),
                       authProperties
                    );


                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {

                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Failed", "Login");
            }

        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("SuccessLogout");
        }



        public ActionResult SuccessLogout()
        {
            return View();
        }




        public ActionResult Failed()
        {
            return View();
        }

        


    }
}