using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coman3.Models
{
    public class Episode : ArchiveItem
    {
        public virtual Season Season { get; set; }
        public string Tags { get; set; }
        public virtual List<VideoLink> VideoLinks { get; set; }

        public Episode()
        {
        }
    }

    public class VideoLink : ArchiveItem
    {
        public string Link { get; set; }
        public int SuccessRate { get; set; }
    }

    public class EpisodeViewModel
    {
        public string Name { get; set; }
        public Guid SeasonId { get; set; }

        public Season Season { get; set; }
    }
}