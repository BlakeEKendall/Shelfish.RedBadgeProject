using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookModels
{
    public class BookListItem
    {
        [Display(Name ="Book ID")]
        public int BookId { get; set; }

        public string Title { get; set; }

        [Display(Name ="Author")]
        public string AuthorName { get; set; }

        [Display(Name ="ISBN")]
        public string Isbn { get; set; }

        
    }
}
