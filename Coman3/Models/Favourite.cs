using System;
using System.ComponentModel.DataAnnotations;
using Coman3.Models.Database;

namespace Coman3.Models
{
    public class Favourite
    {
        [Key]
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public Serie Serie { get; set; }
        public Season Season { get; set; }
        public Episode Episode { get; set; }

    }
}