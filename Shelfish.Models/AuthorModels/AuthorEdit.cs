using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AuthorModels
{
    public class AuthorEdit
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public string CountryName { get; set; }
    }
}
