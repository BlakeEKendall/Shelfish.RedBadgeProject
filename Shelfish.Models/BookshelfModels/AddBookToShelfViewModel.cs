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
        public string SelectedShelfName { get; set; }
        
        [Display(Name ="Selected Book")]
        public int SelectedBookId { get; set; }

    }
}
