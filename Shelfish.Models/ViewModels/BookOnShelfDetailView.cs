using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ViewModels
{
    public class BookOnShelfDetailView
    {
        public int RecordKeeperId { get; set; }
        public int ShelfId { get; set; }

        [Display(Name ="Shelf Name")]
        public string ShelfName { get; set; }

        [Display(Name ="Book ID")]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
