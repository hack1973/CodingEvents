using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag()
        {
        }
    }
}
