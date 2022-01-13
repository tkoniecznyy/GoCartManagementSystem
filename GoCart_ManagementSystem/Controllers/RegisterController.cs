using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models.DTO;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoCart_ManagementSystem.Controllers
{
    public class RegisterController : Controller
    {


        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountService _accountService;

        public RegisterController(ILogger<LoginController> logger, ApplicationDbContext context, IAccountService accountService)
        {
            _logger = logger;
            _dbContext = context;
            _accountService = accountService;
        }

        // GET: Register/NewUser
        public ActionResult Index()
        {
            return View();
        }

        // POST: Register/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterDto registerDto)
        {
            try
            {
                if (ModelState.IsValid & registerDto != null)
                {

                    _accountService.RegisterUser(registerDto);
                    //EmailNotifyService.SendEmail(collection.ElementAt(0).Value.ToString(), collection.ElementAt(1).Value.ToString(), collection.ElementAt(2).Value.ToString());
                    return RedirectToAction(nameof(Success));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Success()
        {
            return View();
        }


    }
}