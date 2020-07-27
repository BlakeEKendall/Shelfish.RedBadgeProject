using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shelfish.Models.BookshelfModels
{
    public class AddAudiobookToShelfViewModel
    {
        public int SelectedShelfId { get; set; }

        [Display(Name = "Selected Audiobook")]
        public int SelectedAudiobookId { get; set; }
        public IEnumerable<SelectListItem> AudiobookListItems { get; set; }
    }
}
