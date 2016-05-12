using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coman3.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Genre()
        {
        }
    }
}