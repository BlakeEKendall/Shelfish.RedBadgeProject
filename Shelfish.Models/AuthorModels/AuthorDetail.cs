﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Models.AuthorModels
{
    public class AuthorDetail
    {
        [Display(Name="Author ID")]
        public int AuthorId { get; set; }

        public string Name { get; set; }

        [Display(Name="Country of Origin")]
        public string CountryName { get; set; }
    }
}
