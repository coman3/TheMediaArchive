using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.Serialization;
using PagedList;

namespace Coman3.Models
{
    [DataContract]
    public class Serie : ArchiveItem
    {
        [DataMember]
        public string ShortDescription { get; set; }
        [DataMember]
        public string LongDescription { get; set; }
        [DataMember]
        public virtual List<Season> Seasons { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Genres { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public int SeasonCount { get; set; }
        [DataMember]
        public int EpisodeCount { get; set; }

        public Serie()
        {

        }

        public Serie(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            if (ShortDescription == null) ShortDescription = string.Empty;
            if (LongDescription == null) LongDescription = string.Empty;
            if (ImageUrl == null) ImageUrl = string.Empty;
        }
    }

    public class SerieViewModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Genres { get; set; }
        public string Tags { get; set; }
    }
}