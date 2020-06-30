using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Bookshelf
    {
        
        public int ShelfId { get; set; }


        public string ShelfName { get; set; }


        public DateTimeOffset CreatedUtc { get; set; }


        public DateTimeOffset? ModifiedUtc { get; set; }


        public int TotalBooks { get; set; }


        public int UserId { get; set; }


        public int BookId { get; set; }

    }
}
