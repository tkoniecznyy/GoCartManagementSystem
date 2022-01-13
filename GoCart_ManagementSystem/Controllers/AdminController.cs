using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoCart_ManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AdminController(ApplicationDbContext context, IAccountService accountService, IUserService userService)
        {
            _context = context;
            _accountService = accountService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction("AccessDenied", "AdminReservation", new { area = "" });
            }
            return View();
        }



        [NonAction]
        public bool CheckPrivilages()
        {
            if (_userService.GetUserRole().Equals("1") || _userService.GetUserRole().Equals("2") || _userService.GetUserRole().Equals("3"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}