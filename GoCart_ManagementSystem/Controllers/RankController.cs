using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoCart_ManagementSystem.Data;
using GoCart_ManagementSystem.Models;
using Newtonsoft.Json.Linq;

namespace GoCart_ManagementSystem.Controllers
{
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rank
        public async Task<IActionResult> Index([FromQuery(Name = "track")] int? track)
        {

            if (track == null)
            {
                track = 1;
            }


            var list = await _context.RankModel.ToListAsync();

            if (track == 2)
            {
                ViewBag.trackId = 2;

                var listTrack2 = list.Where(x => x.TrackId == 2);
                var filter = listTrack2.OrderBy(x => x.Time);

                var first = filter.ElementAt(0).Id;
                var second = filter.ElementAt(1).Id;
                var third = filter.ElementAt(2).Id;

                ViewBag.first = first;
                ViewBag.second = second;
                ViewBag.third = third;

                return View(filter);
            }
            else
            {
                ViewBag.trackId = 1;

                var listTrack1 = list.Where(x => x.TrackId == 1);
                var filter = listTrack1.OrderBy(x => x.Time);

                var first = filter.ElementAt(0).Id;
                var second = filter.ElementAt(1).Id;
                var third = filter.ElementAt(2).Id;

                ViewBag.first = first;
                ViewBag.second = second;
                ViewBag.third = third;

                return View(filter);

            }





        }

        // GET: Rank/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankModel = await _context.RankModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankModel == null)
            {
                return NotFound();
            }

            return View(rankModel);
        }

        // GET: Rank/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rank/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,UserEmail,TrackId,Time,Date")] RankModel rankModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rankModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rankModel);
        }

        // GET: Rank/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankModel = await _context.RankModel.FindAsync(id);
            if (rankModel == null)
            {
                return NotFound();
            }
            return View(rankModel);
        }

        // POST: Rank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,UserEmail,TrackId,Time,Date")] RankModel rankModel)
        {
            if (id != rankModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankModelExists(rankModel.Id))
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
            return View(rankModel);
        }

        // GET: Rank/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankModel = await _context.RankModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rankModel == null)
            {
                return NotFound();
            }

            return View(rankModel);
        }

        // POST: Rank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rankModel = await _context.RankModel.FindAsync(id);
            _context.RankModel.Remove(rankModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankModelExists(int id)
        {
            return _context.RankModel.Any(e => e.Id == id);
        }




        [NonAction]
        [HttpGet]
        public  List<RankModel> RankUnitTestAsync([FromQuery(Name = "track")] int? track)
        {

            if (track == null)
            {
                track = 1;
            }

           

            var list =  _context.RankModel.ToList();

            if (track == 2)
            {
                ViewBag.trackId = 2;

                var listTrack2 = list.Where(x => x.TrackId == 2);
                var filter = listTrack2.OrderBy(x => x.Time);

                var first = filter.ElementAt(0).Id;
                var second = filter.ElementAt(1).Id;
                var third = filter.ElementAt(2).Id;

                ViewBag.first = first;
                ViewBag.second = second;
                ViewBag.third = third;

                 return  filter.ToList();
            }
            else
            {
                ViewBag.trackId = 1;

                var listTrack1 = list.Where(x => x.TrackId == 1);
                var filter = listTrack1.OrderBy(x => x.Time);

                var first = filter.ElementAt(0).Id;
                var second = filter.ElementAt(1).Id;
                var third = filter.ElementAt(2).Id;

                ViewBag.first = first;
                ViewBag.second = second;
                ViewBag.third = third;

               

                return filter.ToList();

            }

        }

     

    }

}    
