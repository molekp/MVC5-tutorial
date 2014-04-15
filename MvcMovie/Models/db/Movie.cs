using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models.db
{
    public class Movie
    {
        public int ID { get; set; }

        [Display(ResourceType=typeof(Resources.Resources),Name="movieTitle" )]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "movieReleaseDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        [Display(ResourceType = typeof(Resources.Resources), Name = "movieGenre")]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Display(ResourceType = typeof(Resources.Resources), Name = "moviePrice")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        [Display(ResourceType = typeof(Resources.Resources), Name = "movieRating")]
        public string Rating { get; set; }
    }
}
