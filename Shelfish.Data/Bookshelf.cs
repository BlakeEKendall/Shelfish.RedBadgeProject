using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Bookshelf
    {
        [Key]
        public int ShelfId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string ShelfName { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        [DefaultValue(0)]
        public int TotalBooks { get; set; }

        

        //Do I need a public int BookId property? or a public virtual Book Book property?
        public virtual ICollection<Book> BooksOnShelf { get; set; }
        public virtual ICollection<Audiobook> AudiobooksOnShelf { get; set; }
    }
}
