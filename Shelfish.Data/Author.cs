using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CountryName { get; set; }

        public ICollection<Book> AuthorBooks { get; set; }
        public ICollection<Audiobook> AuthorAudiobooks { get; set; }
    }
}
