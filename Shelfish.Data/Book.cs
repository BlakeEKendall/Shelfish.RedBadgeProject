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
        public int Isbn { get; set; }

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
        Nonfiction,
        SciFiFantasy,
        Romance,
        RolePlayingGames,
        VideoGames,
        GraphicNovel,
        Manga,
        History,
        ComputersAndProgramming,
        Business,
        Reference,
        Biography,
        ScienceAndTechnology,
        Cooking,
        Religion,
        Philosophy,
        SportsAndOutdoors,
        SelfHelp,
        Parenting,
        ArtAndArchitecture,
        Photography,
        Health,
        Medicine,
        PictureBooks,
        YoungReader,
        YAFiction,
        YASciFiFantasy,
        YARomance,
        YANonfiction,
        Humor,
        Travel,
        Languages,
        HobbiesAndCollecting,
        SocialSciences,
        CurrentAffairs,
        Nature


    }
}
