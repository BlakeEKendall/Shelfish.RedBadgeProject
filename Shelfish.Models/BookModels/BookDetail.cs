using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookModels
{
    public class BookDetail
    {
        [Display(Name ="Book ID")]
        public int BookId { get; set; }

        public string Title { get; set; }

        [Display(Name="Series Title/Number")]
        public string SeriesTitle { get; set; }

        [Display(Name ="Author")]
        public string AuthorName { get; set; }

        [Display(Name ="ISBN")]
        public string Isbn { get; set; }

        public int Rating { get; set; }

        [Display(Name ="Genre")]
        public Genre Genre { get; set; }

        public string Language { get; set; }

        public string Publisher { get; set; }

        [Display(Name ="Is an E-book?")]
        public bool IsEbook { get; set; }
    }
}
