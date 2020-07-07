using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AuthorModels
{
    public class AuthorCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Author name cannot be blank, please enter at least one character.")]
        [MaxLength(100, ErrorMessage = "Name must be fewer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Country name must be at least 4 characters long.")]
        [MaxLength(60, ErrorMessage = "Country name must be fewer than 60 characters.")]
        public string CountryName { get; set; }
    }
}
