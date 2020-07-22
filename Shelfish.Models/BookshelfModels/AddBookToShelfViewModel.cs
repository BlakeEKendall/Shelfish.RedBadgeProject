using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookshelfModels
{
    public class AddBookToShelfViewModel
    {
        public int ShelfRecordId { get; set; }

        public int SelectedShelfId { get; set; }
        public virtual Bookshelf Shelf { get; set; }

        public int SelectedBookId { get; set; }
        public virtual Book Book {get ;set;}
        
    }
}
