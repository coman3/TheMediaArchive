using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Coman3.Models;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace Coman3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Coman3.Models.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Coman3.Models.Database.ApplicationDbContext";
        }

        protected override void Seed(Coman3.Models.Database.ApplicationDbContext context)
        {
            for (int i = 0; i < 500; i++)
            {
                var serie = new Serie("Serie " + i)
                {
                    Seasons = new List<Season>()
                };
                for (int s = 0; s < 4; s++)
                {
                    var season = new Season()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Season " + (s + 1),
                        Number = s + 1,
                        Episodes = new List<Episode>(),
                        Serie = serie,
                    };
                    for (int e = 0; e < 10; e++)
                    {
                        season.Episodes.Add(new Episode
                        {
                            Id = Guid.NewGuid(),
                            Season = season,
                            Name = "Episode " + e,
                        });
                        serie.EpisodeCount++;
                    }
                    serie.SeasonCount++;
                    serie.Seasons.Add(season);
                }
                context.Series.Add(serie);
            }
            context.SaveChanges();
        }
    }
}
