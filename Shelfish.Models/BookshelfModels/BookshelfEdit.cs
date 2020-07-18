﻿using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookshelfModels
{
    public class BookshelfEdit
    {
        [Display(Name = "Shelf ID")]
        public int ShelfId { get; set; }

        [Display(Name ="Shelf Name")]
        public string ShelfName { get; set; }

    }
}
