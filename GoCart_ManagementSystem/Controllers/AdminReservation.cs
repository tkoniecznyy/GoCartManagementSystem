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

namespace GoCart_ManagementSystem.Controllers
{
    [Authorize]
    public class AdminReservation : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AdminReservation(ApplicationDbContext context, IAccountService accountService, IUserService userService)
        {
            _context = context;
            _accountService = accountService;
            _userService = userService;
        }




        // GET: AdminReservation
        public async Task<IActionResult> Index()
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }
            

            return View(await _context.ReservationTable.ToListAsync());
        }

        // GET: AdminReservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.ReservationTable
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // GET: AdminReservation/Create
        public IActionResult Create()
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            return View();
        }

        // POST: AdminReservation/Create
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

            if (dtoCompleted) { 
                _context.ReservationTable.Add(reservationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);
        }

        // GET: AdminReservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.ReservationTable.FindAsync(id);
            if (reservationModel == null)
            {
                return NotFound();
            }
            return View(reservationModel);
        }

        // POST: AdminReservation/Edit/5
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
                    _context.Update(reservationModel);
                    await _context.SaveChangesAsync();
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

        // GET: AdminReservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.ReservationTable
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }



        // POST: AdminReservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationModel = await _context.ReservationTable.FindAsync(id);
            _context.ReservationTable.Remove(reservationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationModelExists(int id)
        {
            return _context.ReservationTable.Any(e => e.ReservationId == id);
        }





        // POST: AdminReservation/PaidConfirmed?reservationId=0&userId=0
        public async Task<IActionResult> PaidConfirmed([FromQuery(Name = "reservationId")] int reservationId, [FromQuery(Name = "userId")] int userId)
        {
            if (!CheckPrivilages())
            {
                return RedirectToAction(nameof(AccessDenied));
            }

            if (reservationId == null)
            {
                return NotFound();
            }
            else
            {
                PaymentModel paymentModel = new PaymentModel();

                paymentModel.Date = DateTime.Now;
                paymentModel.ReservationId = reservationId;
                paymentModel.UserId = userId;

                _context.PaymentTable.Add(paymentModel);
            }

            var reservationModel = await _context.ReservationTable.FindAsync(reservationId);
            if (reservationModel == null)
            {
                return NotFound();
            }
            else
            {
                reservationModel.IsPaid = true;
                reservationModel.PaymentId = _context.PaymentTable.Count()+1;
                await _context.SaveChangesAsync();

            }
            ViewData["resultPaidConfirmed"] = true;
            return RedirectToAction(nameof(Index));
        }



        public IActionResult AccessDenied()
        {
            return View();
        }


        [NonAction]
        public bool CheckPrivilages()
        {
           if( _userService.GetUserRole().Equals("1") || _userService.GetUserRole().Equals("2") || _userService.GetUserRole().Equals("3"))
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
