using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Coman3.Models
{
    [DataContract]
    public class Episode : ArchiveItem
    {
        public virtual Season Season { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public virtual List<VideoLink> VideoLinks { get; set; }

        public Episode()
        {
        }
    }
    [DataContract]
    public class VideoLink : ArchiveItem
    {
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public int SuccessRate { get; set; }
    }

    public class EpisodeViewModel
    {
        public string Name { get; set; }
        public Guid SeasonId { get; set; }

        public Season Season { get; set; }
    }
}