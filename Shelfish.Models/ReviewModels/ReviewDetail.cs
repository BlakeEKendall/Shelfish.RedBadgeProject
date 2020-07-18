using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.ReviewModels
{
    public class ReviewDetail
    {
        [Display(Name = "Review ID")]
        public int ReviewId { get; set; }

        public string Title { get; set; }

        [Display(Name ="Book Reviewed")]
        public string BookTitle { get; set; }

        public string Content { get; set; }

        [Display(Name ="Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name ="Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
