using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class ShelfAudioRecordKeeper
    {
        [Key]
        public int AudioRecordKeeperId { get; set; }

        [ForeignKey("Bookshelf")]
        public int ShelfId { get; set; }
        public virtual Bookshelf Bookshelf { get; set; }

        [ForeignKey("Audiobook")]
        public int AudiobookId { get; set; }
        public virtual Audiobook Audiobook { get; set; }
    }
}
