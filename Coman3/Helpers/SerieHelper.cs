using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Coman3.Models;
using Coman3.Models.Database;

namespace Coman3.Helpers
{
    public class SerieHelper
    {
        public ApplicationDbContext DbContext { get; }

        public SerieHelper(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IQueryable<Serie> GetSeriesFromQuery(SerieIndexBag bag)
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
                        ? series.OrderByDescending(x => x.Seasons.Count) //TODO
                        : series.OrderBy(x => x.Seasons.Count);
                    break;
                case SortOption.EpisodeCount:
                    series = bag.Accending
                        ? series.OrderByDescending(x => x.Seasons.Sum(e => e.Episodes.Count))
                        : series.OrderBy(x => x.Seasons.Sum(e => e.Episodes.Count));
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

            return series;
        }
        public List<Genre> GetGenres(Models.Serie serie)
        {
            return serie.Genres?.Split('|').Select(x => DbContext.Genres.FirstOrDefault(g => g.Name == x)).ToList();
        }
        public List<Genre> GetTags(Models.Serie serie)
        {
            return serie.Genres?.Split('|').Select(x => DbContext.Genres.FirstOrDefault(g => g.Name == x)).ToList();
        }
    }
}