﻿using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shelfish.Models.BookshelfModels
{
    public class AddBookToShelfViewModel
    {
        public int SelectedShelfId { get; set; }

        [Display(Name ="Selected Book")]
        public int SelectedBookId { get; set; }
        public IEnumerable<SelectListItem> BookListItems { get; set; }

    }
}
