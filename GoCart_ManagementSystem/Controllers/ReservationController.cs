using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using GoCart_ManagementSystem.Services;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace GoCart_ManagementSystem.Controllers
{

    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserService _userService;
        private readonly IUserDataService _userDataService;

        public ReservationController(ApplicationDbContext context, IAccountService accountService, IWebHostEnvironment hostingEnvironment, IUserService userService, IUserDataService userDataService)
        {
            _dbContext = context;
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            _userDataService = userDataService;
        }






        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var loggedUserId = _userService.GetHttpContextAccessor().HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reservations = _userDataService.GetUserReservations(loggedUserId);
            var reservationsList = await reservations.ToListAsync();
            var filter = reservationsList.OrderByDescending(x => x.CreationDate);


            return View(filter);
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _dbContext.ReservationTable
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            int max = _dbContext.DiscountCouponTable.Max(p => p.Id);
            var latest = _dbContext.DiscountCouponTable.Where(p => p.Id.Equals(max));
            
            ViewBag.DiscountCoupon = latest.First().DiscountCoupon;
            ViewBag.DiscountValue = latest.First().Value;

            return View();
        }




        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,UserId,UserEmail,ReservationDate,TrackId,ParticipantsCounter,CreationDate,IsPaid,PaymentId,UserComments,PriceInPLN")] ReservationModel reservationModel)
        {
            bool dtoCompleted = false;
            ReservationModel dto = new ReservationModel();
            dto.UserId = reservationModel.UserId;
            dto.UserEmail = reservationModel.UserEmail;
            dto.TrackId = reservationModel.TrackId;
            dto.ParticipantsCounter = reservationModel.ParticipantsCounter;
            dto.CreationDate = reservationModel.CreationDate;
            dto.IsPaid = reservationModel.IsPaid;
            dto.PriceInPLN = reservationModel.PriceInPLN;
            dto.UserComments = reservationModel.UserComments;
            dto.PaymentId = reservationModel.PaymentId;

            if (dto.UserEmail != null)
            {
                dtoCompleted = true;
            }

            if (dtoCompleted)
            {
             
                _dbContext.ReservationTable.Add(reservationModel);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);
        }





        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _dbContext.ReservationTable.FindAsync(id);
            if (reservationModel == null)
            {
                return NotFound();
            }
            return View(reservationModel);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,UserEmail,ReservationDate,TrackId,ParticipantsCounter,CreationDate,IsPaid,PaymentId,UserComments,PriceInPLN")] ReservationModel reservationModel)
        {
            if (id != reservationModel.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(reservationModel);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationModelExists(reservationModel.ReservationId))
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
            return View(reservationModel);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _dbContext.ReservationTable
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationModel = await _dbContext.ReservationTable.FindAsync(id);
            _dbContext.ReservationTable.Remove(reservationModel);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationModelExists(int id)
        {
            return _dbContext.ReservationTable.Any(e => e.ReservationId == id);
        }
    }
}
