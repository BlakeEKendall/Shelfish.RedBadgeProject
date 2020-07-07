using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AuthorModels
{
    public class AuthorListItem
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

    }
}
