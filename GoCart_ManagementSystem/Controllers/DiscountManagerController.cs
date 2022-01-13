using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace GoCart_ManagementSystem.Controllers
{
    [Authorize]
    public class DiscountManagerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public DiscountManagerController(ApplicationDbContext context, IAccountService accountService, IUserService userService)
        {
            _context = context;
            _accountService = accountService;
            _userService = userService;
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


        // GET: DiscountManager
        public async Task<IActionResult> Index()
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction("AccessDenied", "AdminReservation", new { area = "" });
            }

            return View(await _context.DiscountCouponTable.ToListAsync());
        }

        // GET: DiscountManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCouponModel = await _context.DiscountCouponTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCouponModel == null)
            {
                return NotFound();
            }

            return View(discountCouponModel);
        }

        // GET: DiscountManager/Create
        public IActionResult Create()
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction("AccessDenied", "AdminReservation", new { area = "" });
            }

            return View();
        }

        // POST: DiscountManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiscountCoupon,Value,CreationDate")] DiscountCouponModel discountCouponModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountCouponModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCouponModel);
        }

        // GET: DiscountManager/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountCouponModel = await _context.DiscountCouponTable.FindAsync(id);
            if (discountCouponModel == null)
            {
                return NotFound();
            }
            return View(discountCouponModel);
        }

        // POST: DiscountManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiscountCoupon,Value,CreationDate")] DiscountCouponModel discountCouponModel)
        {
            if (id != discountCouponModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountCouponModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCouponModelExists(discountCouponModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discountCouponModel);
        }

        // GET: DiscountManager/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction("AccessDenied", "AdminReservation", new { area = "" });
            }

            if (id == null)
            {
                return NotFound();
            }

            var discountCouponModel = await _context.DiscountCouponTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCouponModel == null)
            {
                return NotFound();
            }

            return View(discountCouponModel);
        }

        // POST: DiscountManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountCouponModel = await _context.DiscountCouponTable.FindAsync(id);
            _context.DiscountCouponTable.Remove(discountCouponModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCouponModelExists(int id)
        {
            return _context.DiscountCouponTable.Any(e => e.Id == id);
        }
    }
}
