using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Audiobook
    {
        [Key]
        public int AudiobookId { get; set; }

        [Required]
        public string Title { get; set; }

        public string SeriesTitle { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string NarratorName { get; set; }

        [Required]
        public AudioFormat AudioFormat { get; set; }

        [Required]
        public bool IsAbridged { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }

    public enum AudioFormat
    {
        [Display(Name ="Mp3 File")]
        Mp3 = 1,

        [Display(Name ="Audio CD")]
        AudioCd,

        [Display(Name ="Audible Audio")]
        Audible
    }
}
