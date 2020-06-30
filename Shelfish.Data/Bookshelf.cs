using System;
using System.Collections.Generic;
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
        //[MaxLength(20, ErrorMessage ="Shelf name must be 20 characters or less.")]
        public string ShelfName { get; set; }

        [Required]
        //[Display(Name ="Date")]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        [Required]
        public int TotalBooks { get; set; }

        

        //Do I need a public int BookId property? or a public virtual Book Book property?
        public virtual ICollection<Book> Books { get; set; }

    }
}
