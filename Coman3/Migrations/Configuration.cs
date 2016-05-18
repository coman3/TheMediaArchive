using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Coman3.Models;
using Newtonsoft.Json;

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
            context.Episodes.RemoveRange(context.Episodes);
            context.Seasons.RemoveRange(context.Seasons);
            context.Series.RemoveRange(context.Series);
            context.SaveChanges();
            for (int i = 0; i < 500; i++)
            {
                var serie = new Serie("Title " + i)
                {
                    ShortDescription = "Short Description for Title " + i + ". It is a great show!",
                };
                var seasons = new List<Season>();
                for (int s = 0; s < 9; s++)
                {
                    var season = new Season
                    {
                        Id = Guid.NewGuid(),
                        Name = "Season " + s,
                        Number = s,
                        Episodes = new List<Episode>()
                    };
                    for (int e = 0; e < 26; e++)
                    {
                        season.Episodes.Add(new Episode
                        {
                            Id = Guid.NewGuid(),
                            Name = "Episode " + e,
                            Season = season,
                        });
                    }
                    seasons.Add(season);
                }
                serie.Seasons = seasons;
                context.Series.Add(serie);
            } 
            context.SaveChanges();
        }
    }
}
