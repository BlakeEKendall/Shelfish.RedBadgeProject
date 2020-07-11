using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AudiobookModels
{
    public class AudiobookCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title must contain at least one character.")]
        [MaxLength(300, ErrorMessage = "Title cannot contain more than 300 characters.")]
        public string Title { get; set; }


        [MaxLength(50, ErrorMessage = "Must contain fewer than 50 characters.")]
        [Display(Name = "Series Name/Number")]
        [DefaultValue("N/A")]
        public string SeriesTitle { get; set; }


        [Required]
        [Display(Name = "Author ID")]
        public int AuthorId { get; set; }


        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN must contain 13 characters.")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }


        [Required]
        [Range(1, 5, ErrorMessage = "Please choose a number between 1 and 5")]
        public int Rating { get; set; }


        [Required]
        [Display(Name = "Genre")]
        public Genre Genre { get; set; }


        [Required]
        [MaxLength(20, ErrorMessage = "Must contain fewer than 20 characters.")]
        public string Language { get; set; }


        [Required]
        [MaxLength(55, ErrorMessage = "Must contain fewer than 55 characters.")]
        public string Publisher { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Narrator cannot be blank, please enter at least one character.")]
        [MaxLength(100, ErrorMessage = "Name must be fewer than 100 characters.")]
        [Display(Name ="Narrator")]
        public string NarratorName { get; set;}


        [Required]
        [Display(Name = "Audio Format")]
        public AudioFormat AudioFormat { get; set; }


        [Required]
        [Display(Name = "Is this abridged?")]
        [DefaultValue(false)]
        public bool IsAbridged { get; set; }
    }
}
