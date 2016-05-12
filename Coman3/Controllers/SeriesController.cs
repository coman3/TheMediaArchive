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
using PagedList;
using WebGrease.Css.Extensions;

namespace Coman3.Controllers
{
    public class SeriesController : Controller
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        // GET: Series
        public async Task<ActionResult> Index(SerieIndexBag bag)
        {
            bag.Validate();
            if (bag.EditMode == false)
            {
                bag.OrderBy = SortOption.Name;
            }
            IQueryable<Serie> series;
            if (bag.Filter.Length < 3)
            {
                series = DbContext.Series;
            }
            else
            {
                series = DbContext.Series.Where(x => x.Name.ToLower().StartsWith(bag.Filter.ToLower()));
            }
            if (series.Count() < bag.ItemsPerPage * bag.Page)
                bag.Page = 1;
            switch (bag.OrderBy)
            {
                case SortOption.Name:
                    series = bag.Accending ? series.OrderBy(x => x.Name) : series.OrderByDescending(x => x.Name);
                    break;
                case SortOption.SeasonCount:
                    series = bag.Accending
                        ? series.OrderByDescending(x => GetSeasonCount(x)) //TODO
                        : series.OrderBy(x => GetSeasonCount(x));
                    break;
                case SortOption.EpisodeCount:
                    series = bag.Accending
                        ? series.OrderByDescending(x => GetEpisodeCount(x))
                        : series.OrderBy(x => GetEpisodeCount(x));
                    break;
                case SortOption.DatePublished:
                    break; //TODO
                case SortOption.LastEpisodeDate:
                    break; //TODO: sort by most recent episode
                case SortOption.DateAdded:
                    break; //TODO: Sort by date added to the database
                case SortOption.MostViews:
                    break; //TODO
                case SortOption.HighestRating:
                    break; //TODO

            }
            
            return View(new SerieIndexViewModel(series.ToPagedList(bag.Page, bag.ItemsPerPage), bag));
        }
        private int GetSeasonCount(Serie serie)
        {
            if (serie.Seasons == null) return 0;
            return serie.Seasons.Count;
        }
        private int GetEpisodeCount(Serie serie)
        {
            if (serie.Seasons == null) return 0;
            return serie.Seasons.Sum(s =>
            {
                if (s.Episodes == null) return 0;
                return s.Episodes.Count;
            });
        }

        // GET: Series/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await DbContext.Series.FindAsync(id);
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
                var dbTags = DbContext.Tags.ToList();
                var dbGenres = DbContext.Genres.ToList();
                
                if (serie.Tags != null)
                {
                    var tags = (serieItem.Tags = serie.Tags.Replace(", ", ",").Replace(" ", "_")).Split(',');
                    tags.ForEach(x =>
                    {
                        var foundTag = dbTags.FirstOrDefault(c => c.Name == x);
                        if (foundTag == null) DbContext.Tags.Add(new Tag { Id = Guid.NewGuid(), Name = x });
                    });
                }
                if (serie.Genres != null)
                {
                    var genres = (serieItem.Genres = serie.Genres.Replace(", ", ",").Replace(" ", "_")).Split(',');
                    genres.ForEach(x =>
                    {
                        var foundGenre = dbGenres.FirstOrDefault(c => c.Name == x);
                        if (foundGenre == null) DbContext.Genres.Add(new Genre { Id = Guid.NewGuid(), Name = x });
                    });
                }
                DbContext.Series.Add(serieItem);
                await DbContext.SaveChangesAsync();
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
            Serie serie = await DbContext.Series.FindAsync(id);
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
                DbContext.Entry(serie).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
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
            Serie serie = await DbContext.Series.FindAsync(id);
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
            Serie serie = await DbContext.Series.FindAsync(id);
            DbContext.Series.Remove(serie);
            await DbContext.SaveChangesAsync();
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
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
