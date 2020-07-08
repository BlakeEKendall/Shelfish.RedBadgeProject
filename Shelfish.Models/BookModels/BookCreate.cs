using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookModels
{
    public class BookCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title must contain at least one character.")]
        [MaxLength(300, ErrorMessage = "Title cannot contain more than 300 characters.")]
        public string Title { get; set; }


        //Figure out how to make this nullable? Or just leave non-required?
        [MaxLength(50, ErrorMessage = "Must contain fewer than 50 characters.")]
        [Display(Name = "Series Name/Number")]
        [DefaultValue(null)]
        public string SeriesTitle { get; set; }


        [Required]
        public int AuthorId { get; set; }


        [Required]
        [StringLength(13, ErrorMessage = "ISBN must contain 13 characters.")]
        public int Isbn { get; set; }


        [Required]
        [Range(1, 5, ErrorMessage = "Please choose a number between 1 and 5")]
        public int Rating { get; set; }


        [Required]
        public Genre Genre { get; set; }


        [Required]
        [MaxLength(20, ErrorMessage = "Must contain fewer than 20 characters.")]
        public string Language { get; set; }


        [Required]
        [MaxLength(55, ErrorMessage = "Must contain fewer than 55 characters.")]
        public string Publisher { get; set; }


        [Required]
        [Display(Name = "Is this an E-book?")]
        [DefaultValue(false)]
        public bool IsEbook { get; set; }

    }
}
