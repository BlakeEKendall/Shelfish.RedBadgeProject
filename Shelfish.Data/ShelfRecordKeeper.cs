using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class ShelfRecordKeeper
    {
        [Key]
        public int RecordKeeperId { get; set; }

        [ForeignKey("Bookshelf")]
        public int ShelfId { get; set; }
        public virtual Bookshelf Bookshelf { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
