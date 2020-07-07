using Shelfish.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.BookModels
{
    public class BookListItem
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public int Isbn { get; set; }

        
    }
}
