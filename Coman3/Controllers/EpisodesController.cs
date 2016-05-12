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
    public class EpisodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Episodes/Details/EpisodeId
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = await db.Episodes.FindAsync(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            return View(episode);
        }

        // GET: Episodes/Create/SeasonId
        public async Task<ActionResult> Create(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var season = await db.Seasons.FindAsync(id);
            if (season == null)
            {
                return HttpNotFound();
            }
            return View(new EpisodeViewModel {Season = season, SeasonId = season.Id, Name = ""});
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,SeasonId")] EpisodeViewModel episode)
        {
            if (ModelState.IsValid)
            {
                var episodeItem = new Episode();
                episodeItem.Id = Guid.NewGuid();
                episodeItem.Name = episode.Name;

                var season = await db.Seasons.FindAsync(episode.SeasonId);
                if (season == null) return HttpNotFound();
                if (season.Episodes == null) season.Episodes = new List<Episode>();
                season.Episodes.Add(episodeItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Series", new { id = season.Serie.Id });
            }

            return View(episode);
        }

        // GET: Episodes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = await db.Episodes.FindAsync(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            return View(episode);
        }

        // POST: Episodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Episode episode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(episode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Episodes", new { id = episode.Id });
            }
            return View(episode);
        }

        // GET: Episodes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episode episode = await db.Episodes.FindAsync(id);
            if (episode == null)
            {
                return HttpNotFound();
            }
            return View(episode);
        }

        // POST: Episodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Episode episode = await db.Episodes.FindAsync(id);
            var season = episode.Season;
            db.Episodes.Remove(episode);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Series", new { id = season.Serie.Id });
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
