using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ReviewModels
{
    public class ReviewEdit
    {
        [Display(Name ="Review ID")]
        public int ReviewId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
