using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Coman3.Models;
using Coman3.Models.Api;
using Coman3.Models.Database;

namespace Coman3.Controllers.Api
{
    public class SerieController : ApiController
    {
        public ApplicationDbContext DbContext = ApplicationDbContext.Create();

        public List<SerieResult> Get(string id)
        {
            var lowId = id.ToLower();
            
            var result =
                DbContext.Series.Where(
                    x =>
                        x.Name.ToLower().StartsWith(lowId))
                    .ToList()
                    .Select(x => new SerieResult(x, GetGenres(x)))
                    .ToList();
            if (result.Count > 0) return result;


            return
                DbContext.Series.Where(
                    x => x.Name.ToLower().Contains(lowId) || x.ShortDescription.ToLower().Contains(lowId))
                    .ToList()
                    .Select(x => new SerieResult(x, GetGenres(x)))
                    .ToList();
        }

        private List<Genre> GetGenres(Serie serie)
        {
            return serie.Genres?.Split('|').Select(x => DbContext.Genres.FirstOrDefault(g=> g.Name == x)).ToList();
        }
        private List<Genre> GetTags(Serie serie)
        {
            return serie.Genres?.Split('|').Select(x => DbContext.Genres.FirstOrDefault(g => g.Name == x)).ToList();
        }

        public void Post()
        {
        }
    }
}
