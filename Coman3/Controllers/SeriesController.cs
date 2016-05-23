using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coman3.Controllers.Api;
using Coman3.Helpers;
using Coman3.Models;
using Coman3.Models.Database;
using PagedList;
using WebGrease.Css.Extensions;

namespace Coman3.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly SerieHelper _serieHelper;

        public SeriesController()
        {
            _serieHelper = new SerieHelper(_dbContext); 
        }

        // GET: Series
        public ActionResult Index(SerieIndexBag bag)
        {
            IQueryable<Serie> series = _serieHelper.GetSeriesFromQuery(bag);

            return View(new SerieIndexViewModel(series.ToPagedList(bag.Page, bag.ItemsPerPage), bag));
        }


        [Authorize]
        public ActionResult Favourites()
        {
            return View();
        }

        // GET: Series/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await _dbContext.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // GET: Series/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortDescription,LongDescription,ImageUrl,Genres,Tags")] SerieViewModel serie)
        {
            if (ModelState.IsValid)
            {
                
                var serieItem = new Serie
                {
                    Id = Guid.NewGuid(),
                    ShortDescription = serie.ShortDescription,
                    LongDescription = serie.LongDescription,
                    ImageUrl = serie.ImageUrl ?? Url.Content("~/Content/Images/Image.jpg"),
                    Name = serie.Name
                };
                var dbTags = _dbContext.Tags.ToList();
                var dbGenres = _dbContext.Genres.ToList();
                
                if (serie.Tags != null)
                {
                    var tags = (serieItem.Tags = serie.Tags.Replace(", ", ",").Replace(" ", "_")).Split(',');
                    tags.ForEach(x =>
                    {
                        var foundTag = dbTags.FirstOrDefault(c => c.Name == x);
                        if (foundTag == null) _dbContext.Tags.Add(new Tag { Id = Guid.NewGuid(), Name = x });
                    });
                }
                if (serie.Genres != null)
                {
                    var genres = (serieItem.Genres = serie.Genres.Replace(", ", ",").Replace(" ", "_")).Split(',');
                    genres.ForEach(x =>
                    {
                        var foundGenre = dbGenres.FirstOrDefault(c => c.Name == x);
                        if (foundGenre == null) _dbContext.Genres.Add(new Genre { Id = Guid.NewGuid(), Name = x });
                    });
                }
                _dbContext.Series.Add(serieItem);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serie);
        }

        // GET: Series/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await _dbContext.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortDescription,LongDescription,ImageUrl")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(serie).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serie);
        }

        // GET: Series/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await _dbContext.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Serie serie = await _dbContext.Series.FindAsync(id);
            _dbContext.Series.Remove(serie);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Watch(Guid? id, int season, int episode)
        {
            //TODO
            if (!id.HasValue) return HttpNotFound();
            return View(new Episode { Id = new Guid(), Name = "Episode 1" });
        }
        [Authorize]
        public ActionResult WatchNext(Guid? id)
        {
            //TODO
            if (!id.HasValue) return HttpNotFound();
            return RedirectToAction("Watch", new { id = id, season = 1, episode = 1 });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
