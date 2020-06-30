using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Audiobook : Book
    {
        [Required]
        public string NarratorName { get; set; }

        [Required]
        public AudioFormat AudioFormat { get; set; }

        [Required]
        public bool IsAbridged { get; set; }
    }

    public enum AudioFormat
    {
        Mp3,
        AudioCd,
        Audible
    }
}
