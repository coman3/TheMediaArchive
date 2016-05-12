using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using PagedList;

namespace Coman3.Models
{
    public class Serie : ArchiveItem
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public virtual List<Season> Seasons { get; set; }
        public string ImageUrl { get; set; }
        public string Genres { get; set; }
        public string Tags { get; set; }

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