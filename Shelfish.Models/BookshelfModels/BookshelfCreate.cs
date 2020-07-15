using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookshelfModels
{
    public class BookshelfCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name ="Shelf Name")]
        public string ShelfName { get; set; }

        public ICollection<Book> BooksOnShelf { get; set; }
    }
}
