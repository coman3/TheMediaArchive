using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Coman3.Models.Api
{
    public class SerieResult
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int SeasonCount { get; set; }
        public int AverageEpisodePerSeason { get; set; }
        public string ShortDescripton { get; set; }
        public string ThumbUrl { get; set; }
        public List<Genre> Genres { get; set; }

        public SerieResult(Serie serie, List<Genre> genres)
        {
            Id = serie.Id;
            Title = serie.Name;
            ShortDescripton = serie.ShortDescription;
            Genres = genres;
            ThumbUrl = serie.ImageUrl + "?width=64&height=84";
            SeasonCount = serie.Seasons?.Count ?? 0;
            AverageEpisodePerSeason =
                serie.Seasons == null || serie.Seasons.Count == 0 ? 0 : (int) serie.Seasons.Average(s => s.Episodes?.Count ?? 0);
        }
    }
}