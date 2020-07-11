using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AudiobookModels
{
    public class AudiobookListItem
    {
        [Display(Name = "Audiobook ID")]
        public int AudiobookId { get; set; }

        public string Title { get; set; }

        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [Display(Name = "ISBN")]
        public string Isbn { get; set; }
    }
}
