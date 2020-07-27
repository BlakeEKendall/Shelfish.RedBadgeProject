using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ViewModels
{
    public class AudiobookOnShelfDetailView
    {
        public int AudioRecordKeeperId { get; set; }
        public int ShelfId { get; set; }

        [Display(Name = "Shelf Name")]
        public string ShelfName { get; set; }

        [Display(Name = "Audiobook ID")]
        public int AudiobookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
