using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coman3.Models;
using Coman3.Models.Database;

namespace Coman3.Controllers
{
    public class SeasonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Seasons/Create
        public async Task<ActionResult> Create(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serie = await db.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(new SeasonViewModel { SeriesId = serie.Id, Serie = serie});
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,SeriesId,Number")] SeasonViewModel season)
        {
            if (ModelState.IsValid)
            {
                var seasonItem = new Season();
                seasonItem.Id = Guid.NewGuid();
                seasonItem.Name = season.Name;
                seasonItem.Number = season.Number;

                var series = await db.Series.FindAsync(season.SeriesId);
                if (series == null) return HttpNotFound();
                if(series.Seasons == null) series.Seasons = new List<Season>();
                series.SeasonCount += 1;
                series.Seasons.Add(seasonItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Series", new { id = series.Id });
            }

            return View(season);
        }

        // GET: Seasons/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Season season = await db.Seasons.FindAsync(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(season);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Season season)
        {
            if (ModelState.IsValid)
            {
                db.Entry(season).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
                return RedirectToAction("Edit", "Series", new { id = season.Serie.Id });
            }
            return View(season);
        }

        // GET: Seasons/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Season season = await db.Seasons.FindAsync(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Season season = await db.Seasons.FindAsync(id);
            var serie = season.Serie;
            db.Seasons.Remove(season);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Series", new { id = serie.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
