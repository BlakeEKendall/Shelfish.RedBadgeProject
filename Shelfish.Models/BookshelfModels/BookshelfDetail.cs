using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookshelfModels
{
    public class BookshelfDetail
    {
        [Display(Name = "Shelf ID")]
        public int ShelfId { get; set; }

        [Display(Name = "Shelf Name")]
        public string ShelfName { get; set; }

        [Display(Name ="Total Books")]
        public int TotalBooks { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        [Display(Name ="Books On Shelf")]
        public virtual ICollection<Book> BooksOnShelf { get; set; }

        [Display(Name = "Audiobooks On Shelf")]
        public virtual ICollection<Audiobook> AudiobooksOnShelf { get; set; }
    }
}
