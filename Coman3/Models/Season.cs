using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Coman3.Models
{
    [DataContract]
    public class Season : ArchiveItem
    {
        
        public virtual Serie Serie { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public virtual List<Episode> Episodes { get; set; }
        [DataMember]
        public string Tags { get; set; }
        public Season()
        {
        }
    }

    public class SeasonViewModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public Guid SeriesId { get; set; }
        public Serie Serie { get; set; }
    }
}