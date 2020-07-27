using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ViewModels
{
    public class AudiobookOnShelfRecordView
    {
        public int AudioRecordKeeperId { get; set; }

        [Display(Name = "Shelf ID")]
        public int ShelfId { get; set; }

        [Display(Name = "Audiobook ID")]
        public int AudiobookId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
    }
}
