using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models.DTO;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoCart_ManagementSystem.Controllers
{
    public class UserSettingsController : Controller
    {

        private readonly ILogger<UserSettingsController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountService _accountService;

        public UserSettingsController(ILogger<UserSettingsController> logger, ApplicationDbContext dbContext, IAccountService accountService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(IFormCollection collection)
        {

            ChangePasswordDto dto = new ChangePasswordDto(collection.ElementAt(0).Value.ToString(), collection.ElementAt(1).Value.ToString(), collection.ElementAt(2).Value.ToString());

            try
            {
                var result = _accountService.ChangePassword(dto);

                if (result.Result)
                {
                    return RedirectToAction(nameof(Logout));
                }
                else
                {
                    return RedirectToAction(nameof(Error));
                }


            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));

            }


        }




        public ActionResult ChangePassword()
        {

            return View();

        }


        public ActionResult Success()
        {

            return View();

        }

        public ActionResult Error()
        {

            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Success");
        }


    }
}