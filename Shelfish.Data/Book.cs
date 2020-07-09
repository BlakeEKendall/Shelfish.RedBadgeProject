using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Shelfish.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string SeriesTitle { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public bool IsEbook { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        
    }

    public enum Genre
    {
        Fiction,

        [Display(Name = "Non-Fiction")]
        Nonfiction,

        [Display(Name = "Sci-Fi/Fantasy")]
        SciFiFantasy,

        Horror,

        Romance,

        [Display(Name = "Role-playing Games")]
        RolePlayingGames,

        [Display(Name = "Video Games")]
        VideoGames,

        [Display(Name = "Graphic Novels"]
        GraphicNovel,

        Manga,

        History,

        [Display(Name ="Computers & Programming")]
        ComputersAndProgramming,

        Business,

        Reference,

        Biography,

        [Display(Name ="Math & Science")]
        ScienceAndMath,

        Cooking,

        Religion,

        Philosophy,

        [Display(Name ="Sports & The Outdoors")]
        SportsAndOutdoors,

        [Display(Name ="Self-help")]
        SelfHelp,

        Parenting,

        [Display(Name ="Art & Architecture")]
        ArtAndArchitecture,

        Photography,

        Health,

        Medicine,

        [Display(Name ="Picture Books")]
        PictureBooks,

        [Display(Name ="Young Reader")]
        YoungReader,

        [Display(Name = "YA Fiction")]
        YAFiction,

        [Display(Name ="YA Sci-Fi/Fantasy")]
        YASciFiFantasy,

        [Display(Name ="YA Romance")]
        YARomance,

        [Display(Name ="YA Non-fiction")]
        YANonfiction,

        Humor,

        Travel,

        Languages,

        Collecting,

        [Display(Name ="Social Sciences & Culture")]
        SocialSciences,

        [Display(Name ="Current Affairs")]
        CurrentAffairs,

        Nature


    }
}
