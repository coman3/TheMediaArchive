using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Coman3.Models.Api
{
    [DataContract]
    public class SerieResult
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int SeasonCount { get; set; }
        [DataMember]
        public int AverageEpisodePerSeason { get; set; }
        [DataMember]
        public string ShortDescripton { get; set; }
        [DataMember]
        public string ThumbUrl { get; set; }
        [DataMember]
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