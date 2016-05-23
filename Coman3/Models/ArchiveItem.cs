using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Coman3.Models
{
    [DataContract]
    public abstract class ArchiveItem
    { 
        [Key]
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}