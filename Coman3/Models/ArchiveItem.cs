using System;
using System.ComponentModel.DataAnnotations;

namespace Coman3.Models
{
    public abstract class ArchiveItem
    { 
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}