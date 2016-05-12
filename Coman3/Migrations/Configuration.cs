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
            var cachePath = @"S:\Files\Projects\Websites\Coman3\Coman3\Content\cache.json";
            var data = JsonConvert.DeserializeObject<List<WatchSeries.Serie>>(File.ReadAllText(cachePath));
            Console.WriteLine(data.Count);
            foreach(var serie in data)
            {
                var newSerie = new Serie(serie.Name)
                {
                    Seasons = new List<Season>(),
                    ShortDescription = $"Short Description for {serie.Name} that is short.",
                    LongDescription = $"Long Description that has lots of detail about {serie.Name}," +
                                      " including details like: " +
                                      "\n - Actors " +
                                      "\n - Genres (and how they are used) " +
                                      "\n - published date " +
                                      "\n - Guessed Finished Date " +
                                      $"\n - Why you should watch {serie.Name}"
                };
                //if (serie.Seasons != null)
                //    foreach (var season in serie.Seasons)
                //    {

                //        var newSeason = new Season
                //        {
                //            Id = Guid.NewGuid(),
                //            Name = season.Name,
                //            Number = season.Number,
                //            Episodes = new List<Episode>()
                //        };
                //        foreach (var episode in season.Episodes)
                //        {
                //            var newEpisode = new Episode
                //            {
                //                Id = Guid.NewGuid(),
                //                Name = episode.Name,
                //                Season = newSeason,
                //                VideoLinks = new List<VideoLink>()
                //            };

                //            if (episode.VideoLinks != null)
                //                foreach (var videoLink in episode.VideoLinks)
                //                {
                //                    newEpisode.VideoLinks.Add(new VideoLink
                //                    {
                //                        Id = Guid.NewGuid(),
                //                        Link = videoLink,
                //                        Name = videoLink
                //                    });
                //                }
                //            newSeason.Episodes.Add(newEpisode);
                //        }
                //        newSerie.Seasons.Add(newSeason);
                //    }
                context.Series.Add(newSerie);
            }
            
            context.SaveChanges();
        }

        //protected override void Seed(Coman3.Models.Database.ApplicationDbContext context)
        //{
        //    //var file = File.ReadAllLines(@"S:\Files\Projects\Websites\Coman3\Coman3\Content\Shows.txt");
        //    //var font = new Font(new FontFamily("Arial"), 16);
        //    //var fontLarge = new Font(new FontFamily("Arial"), 64);
        //    //foreach (var line in file)
        //    //{
        //    //    var serie = new Serie(line);
        //    //    using (var bitmap = new Bitmap(350, 500))
        //    //    using (var g = Graphics.FromImage(bitmap))
        //    //    {
        //    //        g.Clear(Color.Black);
                    
        //    //        g.DrawString(line.Substring(0, 4), fontLarge, Brushes.White, 0, 0);
        //    //        g.DrawString(line, font, Brushes.White, 0, 240);
        //    //        serie.ImageUrl = "Content\\Images\\" + serie.Id + ".jpg";
        //    //        bitmap.Save(@"S:\Files\Projects\Websites\Coman3\Coman3\" + serie.ImageUrl, ImageFormat.Jpeg);
        //    //    }
        //    //    serie.Seasons = new List<Season>();
        //    //    serie.ShortDescription = $"Short Description for {line} that is short.";
        //    //    serie.LongDescription =
        //    //        $"Long Description that has lots of detail about {line}, including details like: \n - Actors \n - Genres (and how they are used) \n - published date \n - Guessed Finished Date \n - Why you should watch {line}";
        //    //    for (int seasonCount = 1; seasonCount < 5; seasonCount++)
        //    //    {
        //    //        var season = new Season
        //    //        {
        //    //            Id = Guid.NewGuid(),
        //    //            Name = "Season " + seasonCount,
        //    //            Number = seasonCount,
        //    //            Serie = serie,
        //    //            Episodes = new List<Episode>()
        //    //        };

        //    //        for (int episodeCount = 1; episodeCount < 24; episodeCount++)
        //    //        {
        //    //            var episode = new Episode
        //    //            {
        //    //                Id = Guid.NewGuid(),
        //    //                Name = "Episode " + episodeCount,
        //    //                Season = season,
        //    //            };
        //    //            season.Episodes.Add(episode);
        //    //        }
        //    //        serie.Seasons.Add(season);
        //    //    }
        //    //    context.Series.Add(serie);
        //    //}
        //    //context.SaveChanges();

        //}
    }
}
