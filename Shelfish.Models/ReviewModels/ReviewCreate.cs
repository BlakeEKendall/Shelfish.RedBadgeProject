using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ReviewModels
{
    public class ReviewCreate
    {
        [Required]
        [Display(Name ="Book ID")]
        public int BookId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(9001)]
        public string Content { get; set; }
    }
}
