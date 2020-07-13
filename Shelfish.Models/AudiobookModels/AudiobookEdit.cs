using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AudiobookModels
{
    public class AudiobookEdit
    {
        [Display(Name = "Audiobook ID")]
        public int AudiobookId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Series Title/Number")]
        public string SeriesTitle { get; set; }

        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        public int Rating { get; set; }

        [Display(Name = "Genre")]
        public Genre Genre { get; set; }

        public string Language { get; set; }

        public string Publisher { get; set; }

        [Display(Name ="Narrator")]
        public string NarratorName { get; set; }

        [Display(Name ="Audio Format")]
        public AudioFormat AudioFormat { get; set; }

        [Display(Name = "Is this abridged?")]
        public bool IsAbridged { get; set; }
    }
}
